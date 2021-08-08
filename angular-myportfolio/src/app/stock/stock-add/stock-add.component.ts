import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StockService } from '../stock.service';

@Component({
  selector: 'app-stock-add',
  templateUrl: './stock-add.component.html',
  styleUrls: ['./stock-add.component.scss']
})

export class StockAddComponent implements OnInit {
  stockForms: FormArray = this.fb.array([]);
  categoryList = [];
  modalityList = [];
  segmentList = [];
  typeofstockList = [];

  constructor(private stockService: StockService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.stockService.getCategories()
    .subscribe(res => this.categoryList = res as []);
    this.stockService.getModalities()
    .subscribe(res => this.modalityList = res as []);
    this.stockService.getSegments()
    .subscribe(res => this.segmentList = res as []);
    this.stockService.getTypesofstock()
    .subscribe(res => this.typeofstockList = res as []);
    this.addStockForm();
  }

  addStockForm() {
    this.stockForms.push(this.fb.group({
      id: [0],
      symbol: ['', Validators.required],
      currentPrice: ['', Validators.required],
      companyName: ['', Validators.required],
      categoryId: [0, Validators.min(1)],
      modalityId: [0, Validators.min(1)],
      segmentId: [0, Validators.min(1)],
      typeOfStockId: [0, Validators.min(1)],
    }));
  }

  get f() { return this.stockForms.controls; }

  recordSubmit(fg: FormGroup) {
      this.stockService.createStock(fg.value).subscribe(
        (res: any) => {
          fg.patchValue({ id: res.id });
          this.router.navigateByUrl('stocks');
        }, error => {
            console.log(error);
          });
        }
}
