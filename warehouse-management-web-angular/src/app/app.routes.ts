import { Routes } from '@angular/router';
import { SignupComponent } from './auth/signup/components/signup.component';
import { SigninComponent } from './auth/signin/components/signin.component';

export const routes: Routes = [
  // Роуты для Auth.
  {
    path: 'signup',
    component: SignupComponent,
  },

  {
    path: 'signin',
    component: SigninComponent,
  },

  {
    path: '',
    redirectTo: '/signin',
    pathMatch: 'full',
  },
];
