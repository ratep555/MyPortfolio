import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurtaxComponent } from './surtax.component';
import { SurtaxAddComponent } from './surtax-add/surtax-add.component';
import { SurtaxEditComponent } from './surtax-edit/surtax-edit.component';

const routes: Routes = [
  {path: '', component: SurtaxComponent},
  {path: 'addsurtax', component: SurtaxAddComponent},
  {path: 'editsurtax/:id', component: SurtaxEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class SurtaxRoutingModule { }
