import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IStock } from 'src/app/shared/models/stock';
import { INewTransaction } from 'src/app/shared/models/transaction';
import { StockService } from 'src/app/stock/stock.service';
import { UsertransactionsService } from '../usertransactions.service';

@Component({
  selector: 'app-usertransactions-buy',
  templateUrl: './usertransactions-buy.component.html',
  styleUrls: ['./usertransactions-buy.component.scss']
})
export class UsertransactionsBuyComponent implements OnInit {
  stockForm: FormGroup;
  stock: IStock;

  constructor(private formBuilder: FormBuilder,
              private stockService: StockService,
              private usertransactionsService: UsertransactionsService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

 ngOnInit(): void {
    this.loadStock();
    this.createStockForm();
  }

createStockForm() {
    this.stockForm = this.formBuilder.group({
    price: ['', [Validators.required]],
    quantity: ['', Validators.required],
    dateOfTransaction: ['', Validators.required]
  });
}

onSubmit() {
  this.usertransactionsService.formData.stockId = this.stock.id;
  this.usertransactionsService.buyStock(this.stockForm.value).subscribe(() => {
    this.resetForm(this.stockForm);
    this.router.navigateByUrl('myportfolio');
  },
  error => {
    console.log(error);
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
