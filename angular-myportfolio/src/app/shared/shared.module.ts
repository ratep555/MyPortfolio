import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PagerComponent } from './components/pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { TextInputComponent } from './components/text-input/text-input.component';
import { GoogleChartsModule } from 'angular-google-charts';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { DateInputComponent } from './components/date-input/date-input.component';

@NgModule({
  declarations: [
    PagerComponent,
    TextInputComponent,
    DateInputComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    TypeaheadModule.forRoot(),
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot(),
    GoogleChartsModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  exports: [
    PaginationModule,
    TypeaheadModule,
    BsDropdownModule,
    PagerComponent,
    CarouselModule,
    ReactiveFormsModule,
    TextInputComponent,
    DateInputComponent,
    GoogleChartsModule,
    BsDatepickerModule
  ]
})
export class SharedModule { }
