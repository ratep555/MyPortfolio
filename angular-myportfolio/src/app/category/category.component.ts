import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ICategory } from '../shared/models/category';
import { MyParams } from '../shared/models/myparams';
import { CategoryService } from './category.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  categories: ICategory[];
  myParams = new MyParams();
  totalCount: number;


  constructor(private categoryService: CategoryService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getCategories();
  }

  getCategories() {
    this.categoryService.getCategories(this.myParams)
    .subscribe(response => {
      this.categories = response.data;
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
    this.getCategories();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getCategories();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getCategories();
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
        this.categoryService.deleteCategory(id)
    .subscribe(
      res => {
        this.getCategories();
        this.toastr.error('Deleted successfully!');
      }, err => { console.log(err);
       });
  }
});
}

}


