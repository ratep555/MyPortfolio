import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TaxliabilityComponent } from './taxliability/taxliability.component';
import { AnnualreviewComponent } from './annualreview.component';

const routes: Routes = [
  {path: '', component: AnnualreviewComponent},
  {path: 'taxliability', component: TaxliabilityComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AnnualreviewRoutingModule { }
