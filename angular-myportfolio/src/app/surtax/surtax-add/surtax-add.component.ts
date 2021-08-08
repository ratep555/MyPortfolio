import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewSurtax } from 'src/app/shared/models/surtax';
import { SurtaxService } from '../surtax.service';

@Component({
  selector: 'app-surtax-add',
  templateUrl: './surtax-add.component.html',
  styleUrls: ['./surtax-add.component.scss']
})
export class SurtaxAddComponent implements OnInit {
  surtaxForm: FormGroup;

  constructor(private surtaxService: SurtaxService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createSurtaxForm();
  }

  createSurtaxForm() {
    this.surtaxForm = this.fb.group({
      residence: ['', [Validators.required]],
      amount: ['', [Validators.required]]
    });
  }

  onSubmit() {
    this.surtaxService.createSurtax(this.surtaxForm.value).subscribe(() => {
      this.resetForm(this.surtaxForm);
      this.router.navigateByUrl('surtaxes');
    },
    error => {
      console.log(error);
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.surtaxService.formData = new INewSurtax();
  }
}



