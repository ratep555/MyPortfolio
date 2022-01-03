import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams, UserParams } from '../shared/models/myparams';
import { IPaginationForStock } from '../shared/models/pagination';
import { map, take } from 'rxjs/operators';
import { INewStockToCreateOrEdit, IStock } from '../shared/models/stock';
import { ICategory } from '../shared/models/category';
import { IModality } from '../shared/models/modality';
import { ISegment } from '../shared/models/segment';
import { ITypeOfStock } from '../shared/models/typeOfStock';
import { AccountService } from '../account/account.service';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  baseUrl = environment.apiUrl;
  user: IUser;
  userParams: UserParams;
  formData: INewStockToCreateOrEdit = new INewStockToCreateOrEdit();

  constructor(private http: HttpClient,
              private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take (1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    });
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = new UserParams(this.user);
    return this.userParams;
  }

  getStocks(userParams: UserParams) {
    let params = new HttpParams();
    if (userParams.categoryId !== 0) {
      params = params.append('categoryId', userParams.categoryId.toString());
    }
    if (userParams.query) {
      params = params.append('query', userParams.query);
    }
    params = params.append('page', userParams.page.toString());
    params = params.append('pageCount', userParams.pageCount.toString());
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
