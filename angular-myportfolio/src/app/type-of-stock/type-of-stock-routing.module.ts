import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TypeOfStockComponent } from './type-of-stock.component';
import { TypeOfStockAddComponent } from './type-of-stock-add/type-of-stock-add.component';
import { TypeOfStockEditComponent } from './type-of-stock-edit/type-of-stock-edit.component';

const routes: Routes = [
  {path: '', component: TypeOfStockComponent},
  {path: 'addtypeofstock', component: TypeOfStockAddComponent},
  {path: 'edittypeofstock/:id', component: TypeOfStockEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class TypeOfStockRoutingModule { }
