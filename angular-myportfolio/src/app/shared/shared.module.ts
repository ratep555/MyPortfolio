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

@NgModule({
  declarations: [
    PagerComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    TypeaheadModule.forRoot(),
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot(),
    GoogleChartsModule.forRoot({ mapsApiKey: 'AIzaSyD-9tSrke72PouQMnMX-a7eZSW0jkFMBWY' })

  ],
  exports: [
    PaginationModule,
    TypeaheadModule,
    BsDropdownModule,
    PagerComponent,
    CarouselModule,
    ReactiveFormsModule,
    TextInputComponent,
    GoogleChartsModule
  ]
})
export class SharedModule { }
