import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsertransactionsComponent } from './usertransactions.component';
import { UsertransactionsBuyComponent } from './usertransactions-buy/usertransactions-buy.component';
import { UsertransactionsSellComponent } from './usertransactions-sell/usertransactions-sell.component';
import { SharedModule } from '../shared/shared.module';
import { UsertransactionsRoutingModule } from './usertransactions-routing.module';

@NgModule({
  declarations: [
    UsertransactionsComponent,
    UsertransactionsBuyComponent,
    UsertransactionsSellComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    UsertransactionsRoutingModule
  ]
})

export class UsertransactionsModule { }
