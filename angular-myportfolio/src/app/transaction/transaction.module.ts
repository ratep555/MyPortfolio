import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionComponent } from './transaction.component';
import { SharedModule } from '../shared/shared.module';
import { TransactionRoutingModule } from './transaction-routing.module';

@NgModule({
  declarations: [
    TransactionComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    TransactionRoutingModule
  ]
})
export class TransactionModule { }
