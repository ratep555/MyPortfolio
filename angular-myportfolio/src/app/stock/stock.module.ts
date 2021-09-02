import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StockComponent } from './stock.component';
import { StockAddComponent } from './stock-add/stock-add.component';
import { StockEditComponent } from './stock-edit/stock-edit.component';
import { SharedModule } from '../shared/shared.module';
import { StockRoutingModule } from './stock-routing.module';

@NgModule({
  declarations: [
    StockComponent,
    StockAddComponent,
    StockEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    StockRoutingModule
  ]

})
export class StockModule { }
