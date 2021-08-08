import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { SegmentService } from '../segment.service';

@Component({
  selector: 'app-segment-edit',
  templateUrl: './segment-edit.component.html',
  styleUrls: ['./segment-edit.component.scss']
})
export class SegmentEditComponent implements OnInit {
  segmentForm: FormGroup;
  id: number;

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private segmentService: SegmentService
) { }

ngOnInit(): void {
  this.id = this.activatedRoute.snapshot.params['id'];

  this.segmentForm = this.formBuilder.group({
    id: [this.id],
    label: new FormControl('', [Validators.required])
    });

  this.segmentService.getSegmentById(this.id)
  .pipe(first())
  .subscribe(x => this.segmentForm.patchValue(x));
}

onSubmit() {
  if (this.segmentForm.invalid) {
      return;
  }
  this.updateSegment();
}

private updateSegment() {
this.segmentService.updateSegment(this.id, this.segmentForm.value)
    .pipe(first())
    .subscribe(() => {
        this.router.navigateByUrl('segments');
      }, error => {
        console.log(error);
      });
    }
}
