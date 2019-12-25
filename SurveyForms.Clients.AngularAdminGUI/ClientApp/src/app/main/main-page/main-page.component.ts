import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../security/auth.service';
import { Router } from '@angular/router';
import { User } from 'oidc-client';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  public isAuth = false;
  public mainContentHeight: number;
  constructor(private authService: AuthService, private route: Router) { }

  ngOnInit() {
    this.mainContentHeight = window.innerHeight - 64;
    this.isAuth = this.authService.isAuth;

    if (this.isAuth) {
      this.route.navigate(['dashboard']);
    }
  }

  public signOut() {
    this.authService.signOut();
  }

  public signIn() {
    this.authService.signIn();
  }

  public showToken() {
    this.authService.getUser().subscribe((_: User) => {
      const date = new Date(0);
      date.setUTCSeconds(_.expires_at);

      if (new Date() >= date) {
        this.authService.renewToken().subscribe();
      }
    });
  }
}
