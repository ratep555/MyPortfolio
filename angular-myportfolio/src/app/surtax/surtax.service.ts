import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForSurtax } from '../shared/models/pagination';
import { map } from 'rxjs/operators';
import { INewSurtax, ISurtax } from '../shared/models/surtax';


@Injectable({
  providedIn: 'root'
})
export class SurtaxService {
  baseUrl = environment.apiUrl;
  formData: INewSurtax = new INewSurtax();

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

  createSurtax(values: any) {
    return this.http.post(this.baseUrl + 'surtaxes', values);
  }

  updateSurtax(id: number, params: any) {
    return this.http.put(`${this.baseUrl}surtaxes/${id}`, params);
  }

  getSurtaxById(id: number) {
    return this.http.get<ISurtax>(`${this.baseUrl}surtaxes/${id}`);
  }

  deleteSurtax(id: number) {
    return this.http.delete(`${this.baseUrl}surtaxes/${id}`);
}

}













