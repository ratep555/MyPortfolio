import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalityComponent } from './modality.component';
import { ModalityAddComponent } from './modality-add/modality-add.component';
import { ModalityEditComponent } from './modality-edit/modality-edit.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ModalityComponent,
    ModalityAddComponent,
    ModalityEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class ModalityModule { }
