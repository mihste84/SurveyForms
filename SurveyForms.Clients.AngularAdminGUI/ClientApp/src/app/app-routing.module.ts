import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './shared/not-found/not-found.component';
import { ErrorComponent } from './shared/error/error.component';
import { UnauthorizedComponent } from './security/unauthorized/unauthorized.component';
import { AuthCallbackComponent } from './security/auth-callback/auth-callback.component';
import { MainPageComponent } from './main/main-page/main-page.component';
import { DashboardPageComponent } from './dashboard/dashboard-page/dashboard-page.component';
import { AuthGuardChildrenService } from './security/auth-guard-children.service';


const routes: Routes = [
  {
    path: '',
    redirectTo: '/',
    pathMatch: 'full'
  },
  {
    path: '',
    component: MainPageComponent,
    canActivateChild: [AuthGuardChildrenService],
    children: [
      {
        path: 'dashboard',
        component: DashboardPageComponent
      }
    ]
  },
  {
    path: 'error',
    component: ErrorComponent
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent
  },
  {
    path: 'authCallback',
    component: AuthCallbackComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
