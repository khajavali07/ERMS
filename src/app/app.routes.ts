import { Routes } from '@angular/router';
import { Departments } from './departments/departments';
import { PageNotFound } from './page-not-found/page-not-found';
import { Login } from './login/login';
import { Settings } from './settings/settings';
import { Payroll } from './payroll/payroll';
import { Dashboard } from './dashboard/dashboard';
import { Layout } from './layout/layout';
import { Employees } from './employees/employees';

export const routes: Routes = [
  //Login
  {path:'login',component:Login},
  //Main Application Layout
  {path:'',component:Layout,children:[
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path:'dashboard',component:Dashboard},
    { path:'employees',component:Employees},
    { path: 'departments', component: Departments },
    { path: 'dashboard', component: Dashboard },
    { path: 'login', component: Login },
    { path: 'settings', component: Settings },
    { path: 'payroll', component: Payroll }
  ]},
  //In-valid URL
  { path: '**', component: PageNotFound }
];
