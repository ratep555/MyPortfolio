import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IModality, INewModality } from '../shared/models/modality';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForModality } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})

export class ModalityService {
  baseUrl = environment.apiUrl;
  formData: INewModality = new INewModality();

  constructor(private http: HttpClient) { }

  getModalities(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForModality>(this.baseUrl + 'modalities', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  createModality(values: any) {
    return this.http.post(this.baseUrl + 'modalities', values);
  }

  updateModality(id: number, params: any) {
    return this.http.put(`${this.baseUrl}modalities/${id}`, params);
  }

  getModalityById(id: number) {
    return this.http.get<IModality>(`${this.baseUrl}modalities/${id}`);
  }

  deleteModality(id: number) {
    return this.http.delete(`${this.baseUrl}modalities/${id}`);
}

}
