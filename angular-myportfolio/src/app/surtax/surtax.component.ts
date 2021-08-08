import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ISurtax } from '../shared/models/surtax';
import { MyParams } from '../shared/models/myparams';
import { SurtaxService } from './surtax.service';
import { ToastrService } from 'ngx-toastr';

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
  if (confirm('Are you sure you want to delete this record?')) {
    this.surtaxService.deleteSurtax(id)
      .subscribe(
        res => {
          this.getSurtaxes();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
}

}








