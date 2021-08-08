import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForStock } from '../shared/models/pagination';
import { map } from 'rxjs/operators';
import { INewStockToCreateOrEdit, IStock } from '../shared/models/stock';
import { ICategory } from '../shared/models/category';
import { IModality } from '../shared/models/modality';
import { ISegment } from '../shared/models/segment';
import { ITypeOfStock } from '../shared/models/typeOfStock';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  baseUrl = environment.apiUrl;
  formData: INewStockToCreateOrEdit = new INewStockToCreateOrEdit();

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

  getStockForEdit(id: number) {
    return this.http.get<INewStockToCreateOrEdit>(this.baseUrl + 'stocks/first/' + id);
  }

  getCategories() {
    return this.http.get<ICategory[]>(this.baseUrl + 'stocks/categories');
  }

  getModalities() {
    return this.http.get<IModality[]>(this.baseUrl + 'stocks/modalities');
  }

  getSegments() {
    return this.http.get<ISegment[]>(this.baseUrl + 'stocks/segments');
  }

  getTypesofstock() {
    return this.http.get<ITypeOfStock[]>(this.baseUrl + 'stocks/typesofstock');
  }

  createStock(formData) {
    return this.http.post(this.baseUrl + 'stocks', formData);
  }

  editStock(formData) {
    return this.http.put(environment.apiUrl + 'stocks/' + formData.id, formData);
  }

  deleteStock(id) {
    return this.http.delete(environment.apiUrl + 'stocks/' + id);
  }

  refreshPrices() {
    return this.http.put(environment.apiUrl + 'stocks/refresh', {});
  }
}
