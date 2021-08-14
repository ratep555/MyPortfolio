import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IAnnualProfitOrLoss } from '../shared/models/annual';
import { MyParams } from '../shared/models/myparams';
import { AnnualreviewService } from './annualreview.service';

@Component({
  selector: 'app-annualreview',
  templateUrl: './annualreview.component.html',
  styleUrls: ['./annualreview.component.scss']
})
export class AnnualreviewComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  annuals: IAnnualProfitOrLoss[];
  myParams = new MyParams();
  totalCount: number;

  constructor(private annualreviewService: AnnualreviewService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAnnuals();
  }

  getAnnuals() {
    this.annualreviewService.getAnnuals(this.myParams)
    .subscribe(response => {
      this.annuals = response.data;
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
    this.getAnnuals();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getAnnuals();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getAnnuals();
    }
}

}
