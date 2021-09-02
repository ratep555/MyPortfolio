import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StockComponent } from './stock.component';
import { StockAddComponent } from './stock-add/stock-add.component';
import { StockEditComponent } from './stock-edit/stock-edit.component';

const routes: Routes = [
  {path: '', component: StockComponent},
  {path: 'addstock', component: StockAddComponent},
  {path: 'editstock/:id', component: StockEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class StockRoutingModule { }
