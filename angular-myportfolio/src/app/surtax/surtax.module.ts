import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SurtaxComponent } from './surtax.component';
import { SurtaxAddComponent } from './surtax-add/surtax-add.component';
import { SurtaxEditComponent } from './surtax-edit/surtax-edit.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    SurtaxComponent,
    SurtaxAddComponent,
    SurtaxEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [SurtaxComponent]
})
export class SurtaxModule { }
