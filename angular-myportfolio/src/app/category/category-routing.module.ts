import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryAddComponent } from './category-add/category-add.component';
import { CategoryEditComponent } from './category-edit/category-edit.component';
import { CategoryComponent } from './category.component';

const routes: Routes = [
  {path: '', component: CategoryComponent},
  {path: 'addcategory', component: CategoryAddComponent},
  {path: 'editcategory/:id', component: CategoryEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class CategoryRoutingModule { }
