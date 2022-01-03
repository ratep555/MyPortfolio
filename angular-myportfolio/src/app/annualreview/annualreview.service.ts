import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IAnnualProfitOrLoss, IAnnualTaxLiability } from '../shared/models/annual';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForAnnual } from '../shared/models/pagination';
import { ISurtax } from '../shared/models/surtax';

@Injectable({
  providedIn: 'root'
})
export class AnnualreviewService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAnnuals(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForAnnual>(this.baseUrl + 'annualReviews/list', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  showAnnual() {
    return this.http.get<IAnnualTaxLiability>(this.baseUrl + 'annualReviews');
  }

  showTaxLiability(id: number) {
    return this.http.get<IAnnualTaxLiability>(this.baseUrl + 'annualReviews/' + id);
  }

  getSurtaxes() {
    return this.http.get<ISurtax[]>(this.baseUrl + 'surtaxes/getsurtaxes');
  }

  getChartWithProfitAndLoss() {
    return this.http.get<any>(this.baseUrl + 'annualReviews/charts').pipe(
    map( result => {
      console.log(result);
      return result;
    })
    );
  }
}










