import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyportfolioComponent } from './myportfolio.component';
import { SharedModule } from '../shared/shared.module';
import { MyportfolioRoutingModule } from './myportfolio-routing.module';
import { ChartsComponent } from './charts/charts.component';

@NgModule({
  declarations: [
    MyportfolioComponent,
    ChartsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MyportfolioRoutingModule
  ]
})

export class MyportfolioModule { }
