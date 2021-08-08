import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UsertransactionsComponent } from './usertransactions.component';
import { StockDetailComponent } from '../stock/stock-detail/stock-detail.component';
import { UsertransactionsBuyComponent } from './usertransactions-buy/usertransactions-buy.component';
import { UsertransactionsSellComponent } from './usertransactions-sell/usertransactions-sell.component';

const routes: Routes = [
  {path: '', component: UsertransactionsComponent},
  {path: ':id', component: StockDetailComponent},
  {path: 'buystock/:id', component: UsertransactionsBuyComponent},
  {path: 'sellstock/:id', component: UsertransactionsSellComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class UsertransactionsRoutingModule { }
