import { Routes } from '@angular/router';
import { ManagerLayoutComponent } from './shared/manager-layout/manager-layout.component';
import { CustomerLayoutComponent } from './shared/customer-layout/customer-layout.component';

export const routes: Routes = [
  {
    path: 'auth',
    component: CustomerLayoutComponent,
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'manager',
    component: ManagerLayoutComponent,
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
