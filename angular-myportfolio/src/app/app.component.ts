import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { IUser } from './shared/models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Myportfolio';


  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadCurrentUser();
 }

  loadCurrentUser() {
  const user: IUser = JSON.parse(localStorage.getItem('user'));
  if (user) {
    this.accountService.setCurrentUser(user);
  }  }
 }
