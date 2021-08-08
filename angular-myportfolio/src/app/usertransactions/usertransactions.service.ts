import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { INewTransaction } from '../shared/models/transaction';

@Injectable({
  providedIn: 'root'
})
export class UsertransactionsService {
  baseUrl = environment.apiUrl;
  formData: INewTransaction = new INewTransaction();

  constructor(private http: HttpClient) { }

  buyStock(values: any) {
    return this.http.post(`${this.baseUrl}transactions/buy/${this.formData.stockId}`, values);
  }

  sellStock(values: any) {
    return this.http.post(`${this.baseUrl}transactions/sell/${this.formData.stockId}`, values);
  }

}
