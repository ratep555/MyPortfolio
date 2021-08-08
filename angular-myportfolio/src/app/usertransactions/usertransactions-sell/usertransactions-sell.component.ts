import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IStock } from 'src/app/shared/models/stock';
import { INewTransaction } from 'src/app/shared/models/transaction';
import { parseWebAPIErrors } from 'src/app/shared/utils';
import { StockService } from 'src/app/stock/stock.service';
import { UsertransactionsService } from '../usertransactions.service';

@Component({
  selector: 'app-usertransactions-sell',
  templateUrl: './usertransactions-sell.component.html',
  styleUrls: ['./usertransactions-sell.component.scss']
})
export class UsertransactionsSellComponent implements OnInit {
  stockForm: FormGroup;
  stock: IStock;
  errors: string[] = [];

  constructor(private stockService: StockService,
              private usertransactionsService: UsertransactionsService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

 ngOnInit(): void {
    this.loadStock();
    this.createStockForm();
  }

createStockForm() {
    this.stockForm = new FormGroup({
    price: new FormControl('', [Validators.required]),
    quantity: new FormControl('', Validators.required)
  });
}

onSubmit() {
  this.usertransactionsService.formData.stockId = this.stock.id;
  this.usertransactionsService.sellStock(this.stockForm.value).subscribe(() => {
    this.resetForm(this.stockForm);
    this.router.navigateByUrl('myportfolio');
  },
  error => {
    console.log(error);
    this.errors = parseWebAPIErrors(error);
  });
}

loadStock() {
  return this.stockService.getStock(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.stock = response;
  }, error => {
    console.log(error);
  });
}

resetForm(form: FormGroup) {
  form.reset();
  this.usertransactionsService.formData = new INewTransaction();
}
}
