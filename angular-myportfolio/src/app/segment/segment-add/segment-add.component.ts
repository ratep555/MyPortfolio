import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewSegment } from 'src/app/shared/models/segment';
import { SegmentService } from '../segment.service';

@Component({
  selector: 'app-segment-add',
  templateUrl: './segment-add.component.html',
  styleUrls: ['./segment-add.component.scss']
})
export class SegmentAddComponent implements OnInit {
  segmentForm: FormGroup;

  constructor(private segmentService: SegmentService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createSegmentForm();
  }

  createSegmentForm() {
    this.segmentForm = this.fb.group({
      label: ['', [Validators.required]]
    });
  }

  onSubmit() {
    this.segmentService.createSegment(this.segmentForm.value).subscribe(() => {
      this.router.navigateByUrl('segments');
    },
    error => {
      console.log(error);
    });
  }

}
