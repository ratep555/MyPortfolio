import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPortfolioAccount, IProfitOrLoss } from '../shared/models/portfolioaccount';

@Injectable({
  providedIn: 'root'
})
export class MyportfolioService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPortfolioAccount(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    return this.http.get<IProfitOrLoss>(this.baseUrl + 'transactions/portfolio', {observe: 'response', params})
    .pipe(
      map(response => {
      return response.body;
          })
        );
      }

  showGraphForClientPortfolio() {
    return this.http.get<any>(this.baseUrl + 'transactions/charts').pipe(
    map( result => {
      console.log(result);
      return result;
    })
    );
  }

  showCountForClientPortfolio() {
    return this.http.get<number>(this.baseUrl + 'transactions/countcharts').pipe(
    map( result => {
      return result;
    })
    );
  }

    }








