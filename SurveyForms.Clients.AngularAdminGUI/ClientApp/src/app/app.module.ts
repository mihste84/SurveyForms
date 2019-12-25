import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { StartupService } from './shared/startup.service';
import { SecurityModule } from './security/security.module';
import { AuthService } from './security/auth.service';
import { MainModule } from './main/main.module';
import { DashboardModule } from './dashboard/dashboard.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    SecurityModule,
    MainModule,
    DashboardModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      // tslint:disable-next-line: only-arrow-functions
      useFactory: (startupService: StartupService, authService: AuthService) => async function() {
        await startupService.getAppInfo();
        await authService.initAuth();

        return;
      },
      deps: [StartupService, AuthService],
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
