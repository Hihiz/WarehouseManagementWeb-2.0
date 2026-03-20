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
import { MeasureUnitComponent } from './directory/measure-unit/components/measure-unit/measure-unit.component';
import { DetailMeasureUnitComponent } from './directory/measure-unit/components/detail-measure-unit/detail-measure-unit.component';
import { CreateMeasureUnitComponent } from './directory/measure-unit/components/create-measure-unit/create-measure-unit.component';
import { DocumentReceiptComponent } from './warehouse/document-receipt/components/document-receipt/document-receipt.component';
import { DetailDocumentReceiptComponent } from './warehouse/document-receipt/components/detail-document-receipt/detail-document-receipt.component';
import { CreateDocumentReceiptComponent } from './warehouse/document-receipt/components/create-document-receipt/create-document-receipt.component';
import { DocumentShipmentComponent } from './warehouse/document-shipment/component/document-shipment/document-shipment.component';
import { DetailDocumentShipmentComponent } from './warehouse/document-shipment/component/detail-document-shipment/detail-document-shipment.component';
import { CreateDocumentShipmentComponent } from './warehouse/document-shipment/component/create-document-shipment/create-document-shipment.component';

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

  // Роуты для MeasureUnit.
  {
    path: 'measure-units',
    component: MeasureUnitComponent,
    canActivate: [authGuard],
  },

  {
    path: 'detail-measure-unit',
    component: DetailMeasureUnitComponent,
    canActivate: [authGuard],
  },

  {
    path: 'create-measure-unit',
    component: CreateMeasureUnitComponent,
    canActivate: [authGuard],
  },

  // Роуты для DocumentReceipt
  {
    path: 'document-receipts',
    component: DocumentReceiptComponent,
    canActivate: [authGuard],
  },

  {
    path: 'detail-document-receipt',
    component: DetailDocumentReceiptComponent,
    canActivate: [authGuard],
  },

  {
    path: 'create-document-receipt',
    component: CreateDocumentReceiptComponent,
    canActivate: [authGuard],
  },

  // Роуты для DocumentShipment
  {
    path: 'document-shipments',
    component: DocumentShipmentComponent,
    canActivate: [authGuard],
  },

  {
    path: 'detail-document-shipment',
    component: DetailDocumentShipmentComponent,
    canActivate: [authGuard],
  },
  {
    path: 'create-document-shipment',
    component: CreateDocumentShipmentComponent,
    canActivate: [authGuard],
  }
];
