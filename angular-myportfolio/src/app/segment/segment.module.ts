import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SegmentComponent } from './segment.component';
import { SegmentAddComponent } from './segment-add/segment-add.component';
import { SegmentEditComponent } from './segment-edit/segment-edit.component';
import { SharedModule } from '../shared/shared.module';
import { SegmentRoutingModule } from './segment-routing.module';



@NgModule({
  declarations: [
    SegmentComponent,
    SegmentAddComponent,
    SegmentEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    SegmentRoutingModule
  ]
})
export class SegmentModule { }
