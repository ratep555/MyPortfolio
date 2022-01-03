import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]
      ],
      password: [null, [Validators.required]],
      confirmPassword: ['', [Validators.required, this.compareValues('password')]]
    });
  }

  compareValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null : {isEqual: true};
    };
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? { emailExists: true } : null;
            })
          );
        })
      );
    };
  }

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.toastr.success('Confirmation link has been sent to your mail');
    }, error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

}










