import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForSurtax } from '../shared/models/pagination';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class SurtaxService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getSurtaxes(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForSurtax>(this.baseUrl + 'surtaxes', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }
}
