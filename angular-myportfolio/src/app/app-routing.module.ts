import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './core/guards/auth.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';

const routes: Routes = [
  {path: '', component: HomeComponent},

  {path: 'not-found', component: NotFoundComponent},

  {path: 'stocks', canActivate: [AuthGuard],
  loadChildren: () => import('./stock/stock.module').then(mod => mod.StockModule),
  data: { roleName: 'Admin' }},

  {path: 'categories', canActivate: [AuthGuard],
  loadChildren: () => import('./category/category.module').then(mod => mod.CategoryModule),
  data: { roleName: 'Admin' }},

  {path: 'modalities', canActivate: [AuthGuard],
  loadChildren: () => import('./modality/modality.module').then(mod => mod.ModalityModule),
  data: { roleName: 'Admin' }},

  {path: 'segments', canActivate: [AuthGuard],
  loadChildren: () => import('./segment/segment.module').then(mod => mod.SegmentModule),
  data: { roleName: 'Admin' }},

  {path: 'surtaxes', canActivate: [AuthGuard],
  loadChildren: () => import('./surtax/surtax.module').then(mod => mod.SurtaxModule),
  data: { roleName: 'Admin' }},

  {path: 'typesofstock', canActivate: [AuthGuard],
  loadChildren: () => import('./type-of-stock/type-of-stock.module').then(mod => mod.TypeOfStockModule),
  data: { roleName: 'Admin' }},

  {path: 'users', canActivate: [AuthGuard],
  loadChildren: () => import('./user/user.module').then(mod => mod.UserModule),
  data: { roleName: 'Admin' }},

  {path: 'usertransactions', canActivate: [AuthGuard],
  loadChildren: () => import('./usertransactions/usertransactions.module').then(mod => mod.UsertransactionsModule)},
  {path: 'myportfolio', canActivate: [AuthGuard],
  loadChildren: () => import('./myportfolio/myportfolio.module').then(mod => mod.MyportfolioModule)},
  {path: 'transactions', canActivate: [AuthGuard],
  loadChildren: () => import('./transaction/transaction.module').then(mod => mod.TransactionModule)},
  {path: 'annual', canActivate: [AuthGuard],
  loadChildren: () => import('./annualreview/annualreview.module').then(mod => mod.AnnualreviewModule)},

  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
