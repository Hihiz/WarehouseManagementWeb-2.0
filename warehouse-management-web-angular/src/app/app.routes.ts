import { Routes } from '@angular/router';
import { SignupComponent } from './auth/signup/components/signup.component';
import { SigninComponent } from './auth/signin/components/signin.component';
import { ClientComponent } from './directory/client/components/client/client.component';
import { DetailClientComponent } from './directory/client/components/detail-client/detail-client.component';
import { CreateClientComponent } from './directory/client/components/create-client/create-client.component';
import { authGuard } from './core/guards/auth.guard';
import { noAuthGuard } from './core/guards/no-auth-guard';
import { ResourceComponent } from './directory/resource/components/resource/resource.component';
import { DetailResourceComponent } from './directory/resource/components/detail-resource/detail-resource.component';
import { CreateResourceComponent } from './directory/resource/components/create-resource/create-resource.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/clients',
    pathMatch: 'full',
  },

  // Роуты для Auth.
  {
    path: 'signup',
    component: SignupComponent,
    canActivate: [noAuthGuard],
  },

  {
    path: 'signin',
    component: SigninComponent,
    canActivate: [noAuthGuard],
  },

  // Роуты для Client.
  {
    path: 'clients',
    component: ClientComponent,
    canActivate: [authGuard],
  },

  {
    path: 'detail-client',
    component: DetailClientComponent,
    canActivate: [authGuard],
  },

  {
    path: 'create-client',
    component: CreateClientComponent,
    canActivate: [authGuard],
  },

  // Роуты для Resource.
  {
    path: 'resources',
    component: ResourceComponent,
    canActivate: [authGuard],
  },

  {
    path: 'detail-resource',
    component: DetailResourceComponent,
    canActivate: [authGuard],
  },

  {
    path: 'create-resource',
    component: CreateResourceComponent,
    canActivate: [authGuard],
  },
];
