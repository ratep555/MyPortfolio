import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ModalityService } from '../modality.service';

@Component({
  selector: 'app-modality-edit',
  templateUrl: './modality-edit.component.html',
  styleUrls: ['./modality-edit.component.scss']
})
export class ModalityEditComponent implements OnInit {
  modalityForm: FormGroup;
  id: number;

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private modalityService: ModalityService
) { }

ngOnInit(): void {
  this.id = this.activatedRoute.snapshot.params['id'];

  this.modalityForm = this.formBuilder.group({
    id: [this.id],
    label: ['', [Validators.required]]
    });

  this.modalityService.getModalityById(this.id)
  .pipe(first())
  .subscribe(x => this.modalityForm.patchValue(x));
}

onSubmit() {
  if (this.modalityForm.invalid) {
      return;
  }
  this.updateModality();
}

private updateModality() {
this.modalityService.updateModality(this.id, this.modalityForm.value)
    .pipe(first())
    .subscribe(() => {
        this.router.navigateByUrl('modalities');
      }, error => {
        console.log(error);
      });
    }
}
