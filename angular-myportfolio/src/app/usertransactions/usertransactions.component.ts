import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MyParams } from '../shared/models/myparams';
import { IStock } from '../shared/models/stock';
import { StockService } from '../stock/stock.service';

@Component({
  selector: 'app-usertransactions',
  templateUrl: './usertransactions.component.html',
  styleUrls: ['./usertransactions.component.scss']
})

export class UsertransactionsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  stocks: IStock[];
  myParams = new MyParams();
  totalCount: number;

  constructor(private stockService: StockService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getStocks();
    this.onRefresh();
  }

  getStocks() {
    this.stockService.getStocks(this.myParams)
    .subscribe(response => {
      this.stocks = response.data;
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
    this.getStocks();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getStocks();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getStocks();
    }
}

onDelete(id: number) {
  if (confirm('Are you sure you want to delete this record?')) {
    this.stockService.deleteStock(id)
      .subscribe(
        res => {
          this.getStocks();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
}

  onRefresh() {
   this.stockService.refreshPrices().subscribe(res => {
   this.getStocks();
  },
   error => {
    console.log(error);
  });
  }

}
