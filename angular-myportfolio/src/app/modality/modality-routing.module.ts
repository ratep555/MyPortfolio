import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModalityComponent } from './modality.component';
import { ModalityAddComponent } from './modality-add/modality-add.component';
import { ModalityEditComponent } from './modality-edit/modality-edit.component';

const routes: Routes = [
  {path: '', component: ModalityComponent},
  {path: 'addmodality', component: ModalityAddComponent},
  {path: 'editmodality/:id', component: ModalityEditComponent}
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class ModalityRoutingModule { }
