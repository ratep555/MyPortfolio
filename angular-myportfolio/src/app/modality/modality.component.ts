import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IModality } from '../shared/models/modality';
import { MyParams } from '../shared/models/myparams';
import { ModalityService } from './modality.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Component({
  selector: 'app-modality',
  templateUrl: './modality.component.html',
  styleUrls: ['./modality.component.scss']
})
export class ModalityComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  modalities: IModality[];
  myParams = new MyParams();
  totalCount: number;


  constructor(private modalityService: ModalityService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getModalities();
  }

  getModalities() {
    this.modalityService.getModalities(this.myParams)
    .subscribe(response => {
      this.modalities = response.data;
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
    this.getModalities();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getModalities();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getModalities();
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
        this.modalityService.deleteModality(id)
    .subscribe(
      res => {
        this.getModalities();
        this.toastr.error('Deleted successfully!');
      }, err => { console.log(err);
       });
  }
});
}

}
