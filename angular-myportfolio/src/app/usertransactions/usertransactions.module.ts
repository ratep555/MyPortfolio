import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsertransactionsComponent } from './usertransactions.component';
import { UsertransactionsBuyComponent } from './usertransactions-buy/usertransactions-buy.component';
import { UsertransactionsSellComponent } from './usertransactions-sell/usertransactions-sell.component';
import { SharedModule } from '../shared/shared.module';
import { UsertransactionsRoutingModule } from './usertransactions-routing.module';
import { StockDetailComponent } from './stock-detail/stock-detail.component';

@NgModule({
  declarations: [
    UsertransactionsComponent,
    UsertransactionsBuyComponent,
    UsertransactionsSellComponent,
    StockDetailComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    UsertransactionsRoutingModule
  ]
})

export class UsertransactionsModule { }
