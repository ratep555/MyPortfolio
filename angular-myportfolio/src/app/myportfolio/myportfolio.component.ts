import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../account/account.service';
import { MyParams } from '../shared/models/myparams';
import { IPortfolioAccount, IProfitOrLoss } from '../shared/models/portfolioaccount';
import { IUser } from '../shared/models/user';
import { MyportfolioService } from './myportfolio.service';

@Component({
  selector: 'app-myportfolio',
  templateUrl: './myportfolio.component.html',
  styleUrls: ['./myportfolio.component.scss']
})
export class MyportfolioComponent implements OnInit {
  currentUser$: Observable<IUser>;
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  portfolioAccount: IPortfolioAccount[];
  totalMarketValue: number;
  totalPriceOfPurchase: number;
  totalProfitOrLoss: number;
  myParams = new MyParams();
  count: number;

  constructor(private myportfolioService: MyportfolioService,
              private accountService: AccountService) { }

  ngOnInit(): void {
  this.currentUser$ = this.accountService.currentUser$;
  this.showCountForClientPortfolio();
  this.getPortfolioAccount();
  }

  getPortfolioAccount() {
    this.myportfolioService.getPortfolioAccount(this.myParams)
    .subscribe(response => {
    this.portfolioAccount = response.clientPortfolios;
    this.totalMarketValue = response.totalMarketValue;
    this.totalPriceOfPurchase = response.totalPriceOfPurchase;
    }, error => console.log(error));
  }

  showCountForClientPortfolio() {
    this.myportfolioService.showCountForClientPortfolio()
    .subscribe(response => {
    this.count = response;
    }, error => console.log(error));
  }

  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getPortfolioAccount();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getPortfolioAccount();
  }

}








