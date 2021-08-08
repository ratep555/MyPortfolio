import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getTransactionsWithProfitAndTraffic(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    return this.http.get(this.baseUrl + 'transactions/listoftransactions', {observe: 'response', params})
    .pipe(
    map(response => {
    return response.body;
        })
      );
    }
}




