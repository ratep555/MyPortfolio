import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { INewStockToCreateOrEdit } from 'src/app/shared/models/stock';
import { StockService } from '../stock.service';

@Component({
  selector: 'app-stock-edit',
  templateUrl: './stock-edit.component.html',
  styleUrls: ['./stock-edit.component.scss']
})
export class StockEditComponent implements OnInit {
  stockForms: FormArray = this.fb.array([]);
  categoryList = [];
  modalityList = [];
  segmentList = [];
  typeofstockList = [];
  id: number;
  stock: INewStockToCreateOrEdit;

  constructor(private stockService: StockService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];

    this.stockService.getCategories()
    .subscribe(res => this.categoryList = res as []);
    this.stockService.getModalities()
    .subscribe(res => this.modalityList = res as []);
    this.stockService.getSegments()
    .subscribe(res => this.segmentList = res as []);
    this.stockService.getTypesofstock()
    .subscribe(res => this.typeofstockList = res as []);

    this.stockService.getStockForEdit(this.id).subscribe(
  (stock: INewStockToCreateOrEdit) => {
    this.stockForms.push(this.fb.group({
              id: [this.id],
              symbol: [stock.symbol, Validators.required],
              currentPrice: [stock.currentPrice, Validators.required],
              companyName: [stock.companyName, Validators.required],
              categoryId: [stock.categoryId, Validators.required],
              modalityId: [stock.modalityId, Validators.required],
              segmentId: [stock.segmentId, Validators.required],
              typeOfStockId: [stock.typeOfStockId, Validators.required]
            }));
      });
  }

  recordSubmit(fg: FormGroup) {
      this.stockService.editStock(fg.value).subscribe(
        (res: any) => {
          this.router.navigateByUrl('stocks');
        }, error => {
            console.log(error);
          });
        }

}
