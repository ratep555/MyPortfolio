import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../account.service';
import { SocialUser } from 'angularx-social-login';
import { ExternalAuth } from 'src/app/shared/models/externalauth';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  returnUrl: string;

  constructor(private accountService: AccountService,
              private fb: FormBuilder,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '';
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators
      .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]],
      password: ['', [Validators.required]]
    });
  }

  public externalLogin = () => {
    this.accountService.signInWithGoogle()
    .then(res => {
      const user: SocialUser = { ...res };
      console.log(user);
      const externalAuth: ExternalAuth = {
        provider: user.provider,
        idToken: user.idToken
      };
      this.validateExternalAuth(externalAuth);
    }, error => console.log(error));
  }

  private validateExternalAuth(externalAuth: ExternalAuth) {
    this.accountService.externalLogin(externalAuth)
      .subscribe(res => {
        this.router.navigateByUrl('/');
      },
      error => {
        console.log(error);
        this.accountService.signOutExternal();
      });
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe(() => {
      this.router.navigateByUrl(this.returnUrl);
    },
     error => {
      console.log(error);
    }
    );
  }
}

