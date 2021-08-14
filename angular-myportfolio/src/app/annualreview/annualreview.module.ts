import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxliabilityComponent } from './taxliability/taxliability.component';
import { SharedModule } from '../shared/shared.module';
import { AnnualreviewRoutingModule } from './annualreview-routing.module';
import { ChartsComponent } from './charts/charts.component';
import { AnnualreviewComponent } from './annualreview.component';



@NgModule({
  declarations: [
    TaxliabilityComponent,
    ChartsComponent,
    AnnualreviewComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AnnualreviewRoutingModule
  ]
})
export class AnnualreviewModule { }
