import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { UserManager, UserManagerSettings, User, UserSettings } from 'oidc-client';
import { StartupService } from '../shared/startup.service';
import { ApiEnvironments } from '../constants/api-environments';
import { devSettings, authTestSettings, stagingSettings } from './auth-settings';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isAuth: boolean;

  private userMngr: UserManager;
  private config: UserManagerSettings;
  private env: string;

  constructor(private startupService: StartupService) {}

  public getUser(): Observable<User> {
    if (this.env === ApiEnvironments.Development) {
      const userSettings: UserSettings = devSettings;
      return new Observable(sub => sub.next(new User(userSettings)));
    }
    return from(this.userMngr.getUser());
  }

  public getUserPromise() {
    if (this.env === ApiEnvironments.Development) {
      const userSettings: UserSettings = devSettings;
      return new User(userSettings);
    }
    return this.userMngr.getUser();
  }

  public async initAuth(): Promise<User> {
    this.env = this.startupService.appInfo.environment;
    this.config = this.getAuthSettings();

    if (this.env === ApiEnvironments.Development) {
      this.isAuth = true;
      const userSettings: UserSettings = devSettings;
      const user = new User(userSettings);

      return user;
    } else {
      this.userMngr = new UserManager(this.config);
      this.userMngr.events.addSilentRenewError((e) => {
        console.log(e);
      });

      const _ = await this.userMngr.getUser();
      this.isAuth = _ != null;
      if (this.isAuth) {
        const date = new Date(0);
        date.setUTCSeconds(_.expires_at);
        if (new Date() >= date) {
          this.isAuth = false;
        }
      }

      return _;
    }
  }

  public signIn(): Observable<any> {
    switch (this.env) {
      case ApiEnvironments.Development: return new Observable();
      case ApiEnvironments.AuthTest: return from(this.userMngr.signinRedirect());
      default: from(this.userMngr.signinPopup().then(_ => window.location.reload()));
    }
  }

  public signOut(): Observable<any> {
    switch (this.env) {
      case ApiEnvironments.Development: return new Observable();
      case ApiEnvironments.AuthTest: return from(this.userMngr.signoutRedirect());
      default: from(this.userMngr.signoutPopup().then(_ => window.location.reload()));
    }
  }

  public renewToken(): Observable<any> {
    if (this.env === ApiEnvironments.Development) {
      return new Observable();
    }

    return from(this.userMngr.signinSilent());
  }

  public authCallback(): Observable<any> {
    if (this.env === ApiEnvironments.Development) {
      return new Observable();
    }

    return from(this.userMngr.signinRedirectCallback());
  }

  private getAuthSettings() {
    switch (this.env) {
      case ApiEnvironments.Development: return devSettings;
      case ApiEnvironments.AuthTest: return authTestSettings;
      case ApiEnvironments.Staging: return stagingSettings;
      default: return null;
    }
  }
}
