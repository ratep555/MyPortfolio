import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MyParams } from '../shared/models/myparams';
import { ITypeOfStock } from '../shared/models/typeOfStock';
import { TypeOfStockService } from './type-of-stock.service';

@Component({
  selector: 'app-type-of-stock',
  templateUrl: './type-of-stock.component.html',
  styleUrls: ['./type-of-stock.component.scss']
})
export class TypeOfStockComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  typesofstock: ITypeOfStock[];
  myParams = new MyParams();
  totalCount: number;

  constructor(private typeofstockService: TypeOfStockService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getTypesOfStock();
  }

  getTypesOfStock() {
    this.typeofstockService.getTypesOfStock(this.myParams)
    .subscribe(response => {
      this.typesofstock = response.data;
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
    this.getTypesOfStock();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getTypesOfStock();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getTypesOfStock();
    }
}

onDelete(id: number) {
  if (confirm('Are you sure you want to delete this record?')) {
    this.typeofstockService.deleteTypeOfStock(id)
      .subscribe(
        res => {
          this.getTypesOfStock();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
}

}

