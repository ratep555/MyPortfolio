import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { SurtaxService } from '../surtax.service';

@Component({
  selector: 'app-surtax-edit',
  templateUrl: './surtax-edit.component.html',
  styleUrls: ['./surtax-edit.component.scss']
})
export class SurtaxEditComponent implements OnInit {
  surtaxForm: FormGroup;
  id: number;

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private surtaxService: SurtaxService
) { }

ngOnInit(): void {
  this.id = this.activatedRoute.snapshot.params['id'];

  this.surtaxForm = this.formBuilder.group({
    id: [this.id],
    residence: ['', [Validators.required]],
    amount: ['', [Validators.required]]
    });

  this.surtaxService.getSurtaxById(this.id)
  .pipe(first())
  .subscribe(x => this.surtaxForm.patchValue(x));
}

onSubmit() {
  if (this.surtaxForm.invalid) {
      return;
  }
  this.updateSurtax();
}

private updateSurtax() {
this.surtaxService.updateSurtax(this.id, this.surtaxForm.value)
    .pipe(first())
    .subscribe(() => {
        this.router.navigateByUrl('surtaxes');
      }, error => {
        console.log(error);
      });
    }
}










