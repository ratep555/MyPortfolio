import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private toastr: ToastrService,
    private router: Router,
    private accountService: AccountService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
          case 400:
              this.handle400Error(error);
            break;
            case 401:
              this.handle401Error(error);
            break;
            case 500:
              this.handle500Error(error);
            break;
             default:
             null;
          }
        }
        return throwError(error);
      })
    );
  }

  handle400Error(error: any) {
    if (error.error.errors) {
        throw error.error;
    }  else {
        this.toastr.error(error.error.message, error.error.statusCode);
    }
}
  handle401Error(error: any) {
    const errorMessage = 'Unauthorized!';
    this.accountService.logout();
    this.toastr.error(errorMessage, error.status);
    this.router.navigate(['/account/login']);
  }

  handle500Error(error: any) {
    this.toastr.error('Please contact the administrator. An error happened in the server.');
    console.log(error);
  }
}
