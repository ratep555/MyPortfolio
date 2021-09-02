import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from '../shared/shared.module';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [NavBarComponent, NotFoundComponent],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
    CollapseModule.forRoot()

  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
