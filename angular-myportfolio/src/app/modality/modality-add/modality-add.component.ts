import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewModality } from 'src/app/shared/models/modality';
import { ModalityService } from '../modality.service';

@Component({
  selector: 'app-modality-add',
  templateUrl: './modality-add.component.html',
  styleUrls: ['./modality-add.component.scss']
})
export class ModalityAddComponent implements OnInit {
  modalityForm: FormGroup;

  constructor(private modalityService: ModalityService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createModalityForm();
  }

  createModalityForm() {
    this.modalityForm = this.fb.group({
      label: ['', [Validators.required]]
    });
  }

  onSubmit() {
    this.modalityService.createModality(this.modalityForm.value).subscribe(() => {
      this.router.navigateByUrl('modalities');
    },
    error => {
      console.log(error);
    });
  }

}
