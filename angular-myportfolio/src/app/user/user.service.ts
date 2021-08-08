import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForUser } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForUser>(this.baseUrl + 'account/users', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  unlockUser(userId: string) {
    return this.http.put(`${this.baseUrl}account/unlock/${userId}`, {});
}

  lockUser(userId: string) {
    return this.http.put(`${this.baseUrl}account/lock/${userId}`, {});
}
}
