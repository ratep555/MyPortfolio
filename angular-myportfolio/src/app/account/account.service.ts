import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ForgotPassword } from '../shared/models/forgotpassword';
import { ResetPassword } from '../shared/models/resetpassword';
import { IUser } from '../shared/models/user';
import { SocialAuthService } from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>(JSON.parse(localStorage.getItem('user')));
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient,
              private router: Router,
              private externalAuthService: SocialAuthService) { }

  public get userValue(): IUser {
    return this.currentUserSource.value;
}

  login(values: any) {
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
    map((user: IUser) => {
      if (user) {
        this.setCurrentUser(user);

      }
    })
    );
}

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((user: IUser) => {
        if (user) {
          this.setCurrentUser(user);

        }
      })
    );
  }

  public signInWithGoogle = () => {
    return this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  externalLogin(values: any) {
    return this.http.post(this.baseUrl + 'account/externallogin', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  public signOutExternal = () => {
    this.externalAuthService.signOut();
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get(this.baseUrl + 'account/emailexists?email=' + email);
  }

  forgotPassword(forgotpassword: ForgotPassword) {
    return this.http.post(this.baseUrl + 'account/forgotpassword', forgotpassword);
  }

  resetPassword(resetpassword: ResetPassword) {
    return this.http.post(this.baseUrl + 'account/resetpassword', resetpassword);
  }

  setCurrentUser(user: IUser) {
    user.role = this.getDecodedToken(user.token).role;
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }

}







