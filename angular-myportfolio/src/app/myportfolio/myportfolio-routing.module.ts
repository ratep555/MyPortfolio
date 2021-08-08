import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MyportfolioComponent } from './myportfolio.component';

const routes: Routes = [
  {path: '', component: MyportfolioComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class MyportfolioRoutingModule { }
