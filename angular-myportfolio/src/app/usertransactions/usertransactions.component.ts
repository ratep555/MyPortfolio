import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { kkLocale } from 'ngx-bootstrap/chronos';
import { ToastrService } from 'ngx-toastr';
import { ICategory } from '../shared/models/category';
import { UserParams } from '../shared/models/myparams';
import { IStock } from '../shared/models/stock';
import { StockService } from '../stock/stock.service';

@Component({
  selector: 'app-usertransactions',
  templateUrl: './usertransactions.component.html',
  styleUrls: ['./usertransactions.component.scss']
})

export class UsertransactionsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  stocks: IStock[];
  userParams: UserParams;
  totalCount: number;
  categories: ICategory[];

  constructor(private stockService: StockService,
              private toastr: ToastrService)
    {this.userParams = this.stockService.getUserParams(); }

  ngOnInit(): void {
    this.getStocks();
    this.getCategories();
    this.onRefresh();
  }

  getStocks() {
    this.stockService.setUserParams(this.userParams);
    this.stockService.getStocks(this.userParams)
    .subscribe(response => {
      this.stocks = response.data;
      this.userParams.page = response.page;
      this.userParams.pageCount = response.pageCount;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  getCategories() {
    this.stockService.getCategories().subscribe(response => {
    this.categories = response;
    }, error => {
    console.log(error);
    });
    }

  onCategorySelected(categoryIdId: number) {
    this.userParams.categoryId = categoryIdId;
    this.getStocks();
    }

  onSearch() {
    this.userParams.query = this.searchTerm.nativeElement.value;
    this.getStocks();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.userParams = this.stockService.resetUserParams();
    this.getStocks();
  }

  onReset1() {
    this.filterTerm.nativeElement.value = '';
    this.userParams = this.stockService.resetUserParams();
    this.getStocks();
  }

  onPageChanged(event: any) {
    if (this.userParams.page !== event) {
      this.userParams.page = event;
      this.stockService.setUserParams(this.userParams);
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
