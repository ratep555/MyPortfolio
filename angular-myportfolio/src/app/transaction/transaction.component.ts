import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MyParams } from '../shared/models/myparams';
import { ITransactionForUser, ITransactionsWithProfitAndTraffic } from '../shared/models/transactionsForUser';
import { TransactionService } from './transaction.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  myParams = new MyParams();
  listOfTransactions: ITransactionForUser[];
  totalNetProfit: number;
  totalTraffic: number;

  constructor(private transactionService: TransactionService) { }

  ngOnInit(): void {
    this.getTransactionsForUserWithProfitAndTraffic();
  }

  getTransactionsForUserWithProfitAndTraffic() {
    this.transactionService.getTransactionsWithProfitAndTraffic(this.myParams).
    subscribe((data: ITransactionsWithProfitAndTraffic) => {
      this.listOfTransactions = data.listOfTransactions;
      this.totalNetProfit = data.totalNetProfit;
      this.totalTraffic = data.totalTraffic;
    }, error => {
      console.log(error);
    });
  }

  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getTransactionsForUserWithProfitAndTraffic();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getTransactionsForUserWithProfitAndTraffic();
  }

}







