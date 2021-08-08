import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IModality } from '../shared/models/modality';
import { MyParams } from '../shared/models/myparams';
import { ModalityService } from './modality.service';

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
  if (confirm('Are you sure you want to delete this record?')) {
    this.modalityService.deleteModality(id)
      .subscribe(
        res => {
          this.getModalities();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
}

}
