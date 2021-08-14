import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ISurtax } from '../shared/models/surtax';
import { MyParams } from '../shared/models/myparams';
import { SurtaxService } from './surtax.service';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Component({
  selector: 'app-surtax',
  templateUrl: './surtax.component.html',
  styleUrls: ['./surtax.component.scss']
})

export class SurtaxComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  surtaxes: ISurtax[];
  myParams = new MyParams();
  totalCount: number;


  constructor(private surtaxService: SurtaxService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getSurtaxes();
  }

  getSurtaxes() {
    this.surtaxService.getSurtaxes(this.myParams)
    .subscribe(response => {
      this.surtaxes = response.data;
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
    this.getSurtaxes();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getSurtaxes();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getSurtaxes();
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
        this.surtaxService.deleteSurtax(id)
    .subscribe(
      res => {
        this.getSurtaxes();
        this.toastr.error('Deleted successfully!');
      }, err => { console.log(err);
       });
  }
});
}

}








