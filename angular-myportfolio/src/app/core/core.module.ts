import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from '../shared/shared.module';




@NgModule({
  declarations: [NavBarComponent],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
