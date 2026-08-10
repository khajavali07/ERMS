import { Routes } from '@angular/router';
import { Departments } from './departments/departments';
import { Home } from './home/home';
import { PageNotFound } from './page-not-found/page-not-found';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'departments', component: Departments },
  {path:'**',component:PageNotFound}
];
