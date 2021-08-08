import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MyParams } from '../shared/models/myparams';
import { ISegment } from '../shared/models/segment';
import { SegmentService } from './segment.service';

@Component({
  selector: 'app-segment',
  templateUrl: './segment.component.html',
  styleUrls: ['./segment.component.scss']
})

export class SegmentComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  segments: ISegment[];
  myParams = new MyParams();
  totalCount: number;

  constructor(private segmentService: SegmentService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getSegments();
  }

  getSegments() {
    this.segmentService.getSegments(this.myParams)
    .subscribe(response => {
      this.segments = response.data;
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
    this.getSegments();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getSegments();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getSegments();
    }
}

onDelete(id: number) {
  if (confirm('Are you sure you want to delete this record?')) {
    this.segmentService.deleteSegment(id)
      .subscribe(
        res => {
          this.getSegments();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
}

}
