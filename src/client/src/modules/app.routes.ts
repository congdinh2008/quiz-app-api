import { Routes } from '@angular/router';
import { ManagerLayoutComponent } from './shared/manager-layout/manager-layout.component';
import { CustomerLayoutComponent } from './shared/customer-layout/customer-layout.component';
import { AuthGuard } from '../guards/auth.guard';
import { Error403Component } from './shared/error-403/error-403.component';

export const routes: Routes = [
  {
    path: 'error',
    component: CustomerLayoutComponent,
    children: [
      {
        path: '403',
        component: Error403Component,
      },
    ],
  },
  {
    path: 'auth',
    component: CustomerLayoutComponent,
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'manager',
    component: ManagerLayoutComponent,
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./manager/manager.module').then((m) => m.ManagerModule),
  },
  {
    path: '',
    component: CustomerLayoutComponent,
    loadChildren: () =>
      import('./customer/customer.module').then((m) => m.CustomerModule),
  },
];
