import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MyParams } from '../shared/models/myparams';
import { IUser1 } from '../shared/models/user';
import { UserService } from './user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  users: IUser1[];
  myParams = new MyParams();
  totalCount: number;

  constructor(private userService: UserService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers(this.myParams)
    .subscribe(response => {
      this.users = response.data;
      this.myParams.page = response.page;
      this.myParams.pageCount = response.pageCount;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getUsers();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getUsers();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getUsers();
    }
}

unlockUser(userId: string) {
  if (confirm('Are you sure you want to unlock this user?')) {
    this.userService.unlockUser(userId)
      .subscribe(
        res => {
          this.getUsers();
          this.toastr.success('User Unlocked!!');
        },
        err => { console.log(err);
         }
      );
  }
}

lockUser(userId: string) {
  if (confirm('Are you sure you want to lock this user?')) {
    this.userService.lockUser(userId)
      .subscribe(
        res => {
          this.getUsers();
          this.toastr.error('User Locked!!');
        },
        err => { console.log(err);
         }
      );
  }
}
}
