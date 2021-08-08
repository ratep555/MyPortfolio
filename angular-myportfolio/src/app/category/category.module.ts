import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category.component';
import { CategoryAddComponent } from './category-add/category-add.component';
import { CategoryEditComponent } from './category-edit/category-edit.component';
import { SharedModule } from '../shared/shared.module';
import { SurtaxRoutingModule } from '../surtax/surtax-routing.module';
import { CategoryRoutingModule } from './category-routing.module';



@NgModule({
  declarations: [
    CategoryComponent,
    CategoryAddComponent,
    CategoryEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CategoryRoutingModule
  ]})

export class CategoryModule { }
