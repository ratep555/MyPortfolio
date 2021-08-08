import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SegmentComponent } from './segment.component';
import { SegmentAddComponent } from './segment-add/segment-add.component';
import { SegmentEditComponent } from './segment-edit/segment-edit.component';

const routes: Routes = [
  {path: '', component: SegmentComponent},
  {path: 'addsegment', component: SegmentAddComponent},
  {path: 'editsegment/:id', component: SegmentEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class SegmentRoutingModule { }
