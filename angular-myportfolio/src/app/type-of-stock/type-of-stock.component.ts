import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MyParams } from '../shared/models/myparams';
import { ITypeOfStock } from '../shared/models/typeOfStock';
import { TypeOfStockService } from './type-of-stock.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';

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
        this.typeofstockService.deleteTypeOfStock(id)
    .subscribe(
      res => {
        this.getTypesOfStock();
        this.toastr.error('Deleted successfully!');
      }, err => { console.log(err);
       });
  }
});
}

}

