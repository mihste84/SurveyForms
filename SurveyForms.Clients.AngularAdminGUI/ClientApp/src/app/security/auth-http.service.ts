import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { User } from 'oidc-client';
import { map, flatMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthHttpService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  public get<M>(url: string, options: {
      headers?: HttpHeaders | {[header: string]: string | string[]},
      observe?: 'body',
      params?: HttpParams|{[param: string]: string | string[]},
      reportProgress?: boolean,
      responseType?: 'json',
      withCredentials?: boolean,
    } = {}): Observable<M> {
    return this.authService.getUser()
    .pipe(
      map((user: User) => this.addAuthToOptions(user, options)),
      flatMap(opts => this.http.get<M>(url, opts)),
      map(_ => _)
    );
  }

  public delete<M>(url: string, options: {
    headers?: HttpHeaders | {[header: string]: string | string[]},
    observe?: 'body',
    params?: HttpParams|{[param: string]: string | string[]},
    reportProgress?: boolean,
    responseType?: 'json',
    withCredentials?: boolean,
  } = {}): Observable<M> {
  return this.authService.getUser()
  .pipe(
    map((user: User) => this.addAuthToOptions(user, options)),
    flatMap(opts => this.http.delete<M>(url, opts)),
    map(_ => _)
  );
}

  public put<M>(url: string, body?: any, options: {
    headers?: HttpHeaders | {[header: string]: string | string[]},
    observe?: 'body',
    params?: HttpParams|{[param: string]: string | string[]},
    reportProgress?: boolean,
    responseType?: 'json',
    withCredentials?: boolean,
  } = {}): Observable<M> {
    return this.authService.getUser()
    .pipe(
      map((user: User) => this.addAuthToOptions(user, options)),
      flatMap(opts => this.http.put<M>(url, body, opts))
    );
  }

  public post<M>(url: string, body?: any, options: {
    headers?: HttpHeaders | {[header: string]: string | string[]},
    observe?: 'body',
    params?: HttpParams|{[param: string]: string | string[]},
    reportProgress?: boolean,
    responseType?: 'json',
    withCredentials?: boolean,
  } = {}): Observable<M> {
    return this.authService.getUser()
    .pipe(
      map((user: User) => this.addAuthToOptions(user, options)),
      flatMap(opts => this.http.post<M>(url, body, opts))
    );
  }

  private addAuthToOptions(user: User, options: {
    headers?: HttpHeaders | {[header: string]: string | string[]},
    observe?: 'body',
    params?: HttpParams|{[param: string]: string | string[]},
    reportProgress?: boolean,
    responseType?: 'json',
    withCredentials?: boolean,
  } = {}) {
    options.headers = new HttpHeaders({ Authorization: `${user.token_type} ${user.access_token}` });
    return options;
  }
}
