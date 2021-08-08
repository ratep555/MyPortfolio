import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForTypeOfStock } from '../shared/models/pagination';
import { INewTypeOfStock, ITypeOfStock } from '../shared/models/typeOfStock';

@Injectable({
  providedIn: 'root'
})
export class TypeOfStockService {
  baseUrl = environment.apiUrl;
  formData: INewTypeOfStock = new INewTypeOfStock();

  constructor(private http: HttpClient) { }

  getTypesOfStock(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForTypeOfStock>(this.baseUrl + 'typesofstock', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  createTypeOfStock(values: any) {
    return this.http.post(this.baseUrl + 'typesofstock', values);
  }

  updateTypeOfStock(id: number, params: any) {
    return this.http.put(`${this.baseUrl}typesofstock/${id}`, params);
  }

  getTypeOfStockById(id: number) {
    return this.http.get<ITypeOfStock>(`${this.baseUrl}typesofstock/${id}`);
  }

  deleteTypeOfStock(id: number) {
    return this.http.delete(`${this.baseUrl}typesofstock/${id}`);
}

}
