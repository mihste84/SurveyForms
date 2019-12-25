import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [UnauthorizedComponent, AuthCallbackComponent],
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule
  ]
})
export class SecurityModule { }
