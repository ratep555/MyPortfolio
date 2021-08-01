import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StockComponent } from './stock.component';
import { StockDetailComponent } from './stock-detail/stock-detail.component';

const routes: Routes = [
  {path: '', component: StockComponent},
  {path: ':id', component: StockDetailComponent},

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class StockRoutingModule { }
