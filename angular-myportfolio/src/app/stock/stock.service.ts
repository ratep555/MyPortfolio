import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForStock } from '../shared/models/pagination';
import { map } from 'rxjs/operators';
import { IStock } from '../shared/models/stock';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getStocks(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForStock>(this.baseUrl + 'stocks', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getStock(id: number) {
    return this.http.get<IStock>(this.baseUrl + 'stocks/' + id);
  }

  refreshPrices() {
    return this.http.put(environment.apiUrl + 'stocks/refresh', {});
  }
}
