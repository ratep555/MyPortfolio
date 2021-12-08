import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { IStock } from '../shared/models/stock';
import { MyParams } from '../shared/models/myparams';
import { StockService } from './stock.service';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { AccountService } from '../account/account.service';
import { IUser } from '../shared/models/user';
import { ICategory } from '../shared/models/category';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.scss']
})
export class StockComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  stocks: IStock[];
  myParams = new MyParams();
  totalCount: number;
  categories: ICategory[];

  constructor(private stockService: StockService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getStocks();
    this.getCategories();
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

  getCategories() {
    this.stockService.getCategories().subscribe(response => {
    this.categories = response;
    }, error => {
    console.log(error);
    });
    }

  onCategorySelected(categoryIdId: number) {
    this.myParams.categoryId = categoryIdId;
    this.getStocks();
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

  onReset1() {
    this.filterTerm.nativeElement.value = '';
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
  Swal.fire({
    title: 'Are you sure want to delete this record?',
    text: 'You will not be able to recover it afterwards!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Yes, delete it!',
    confirmButtonColor: '#DD6B55',
    cancelButtonText: 'No, keep it'
  }).then((result) => {
    if (result.value) {
        this.stockService.deleteStock(id)
    .subscribe(
      res => {
        this.getStocks();
        this.toastr.error('Deleted successfully!');
      }, err => { console.log(err);
       });
  }
});
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






