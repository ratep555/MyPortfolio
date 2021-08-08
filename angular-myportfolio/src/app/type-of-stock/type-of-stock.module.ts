import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TypeOfStockComponent } from './type-of-stock.component';
import { TypeOfStockAddComponent } from './type-of-stock-add/type-of-stock-add.component';
import { TypeOfStockEditComponent } from './type-of-stock-edit/type-of-stock-edit.component';
import { SharedModule } from '../shared/shared.module';
import { TypeOfStockRoutingModule } from './type-of-stock-routing.module';



@NgModule({
  declarations: [
    TypeOfStockComponent,
    TypeOfStockAddComponent,
    TypeOfStockEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    TypeOfStockRoutingModule
  ]
})
export class TypeOfStockModule { }
