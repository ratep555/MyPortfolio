import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForCategory, IPaginationForSurtax } from '../shared/models/pagination';
import { map } from 'rxjs/operators';
import { ICategory, INewCategory } from '../shared/models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;
  formData: INewCategory = new INewCategory();

  constructor(private http: HttpClient) { }

  getCategories(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForCategory>(this.baseUrl + 'categories', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  createCategory(values: any) {
    return this.http.post(this.baseUrl + 'categories', values);
  }

  updateCategory(id: number, params: any) {
    return this.http.put(`${this.baseUrl}categories/${id}`, params);
  }

  getCategoryById(id: number) {
    return this.http.get<ICategory>(`${this.baseUrl}categories/${id}`);
  }

  deleteCategory(id: number) {
    return this.http.delete(`${this.baseUrl}categories/${id}`);
}

}



