import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './core/guards/auth.guard';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'stocks', canActivate: [AuthGuard],
  loadChildren: () => import('./stock/stock.module').then(mod => mod.StockModule)},
  {path: 'categories', canActivate: [AuthGuard],
  loadChildren: () => import('./category/category.module').then(mod => mod.CategoryModule)},
  {path: 'modalities', canActivate: [AuthGuard],
  loadChildren: () => import('./modality/modality.module').then(mod => mod.ModalityModule)},
  {path: 'segments', canActivate: [AuthGuard],
  loadChildren: () => import('./segment/segment.module').then(mod => mod.SegmentModule)},
  {path: 'surtaxes', canActivate: [AuthGuard],
  loadChildren: () => import('./surtax/surtax.module').then(mod => mod.SurtaxModule)},
  {path: 'typesofstock', canActivate: [AuthGuard],
  loadChildren: () => import('./type-of-stock/type-of-stock.module').then(mod => mod.TypeOfStockModule)},
  {path: 'users', canActivate: [AuthGuard],
  loadChildren: () => import('./user/user.module').then(mod => mod.UserModule)},
  {path: 'usertransactions', canActivate: [AuthGuard],
  loadChildren: () => import('./usertransactions/usertransactions.module').then(mod => mod.UsertransactionsModule)},
  {path: 'myportfolio', canActivate: [AuthGuard],
  loadChildren: () => import('./myportfolio/myportfolio.module').then(mod => mod.MyportfolioModule)},
  {path: 'transactions', canActivate: [AuthGuard],
  loadChildren: () => import('./transaction/transaction.module').then(mod => mod.TransactionModule)},

  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
