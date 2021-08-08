import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TransactionComponent } from './transaction.component';

const routes: Routes = [
  {path: '', component: TransactionComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class TransactionRoutingModule { }
