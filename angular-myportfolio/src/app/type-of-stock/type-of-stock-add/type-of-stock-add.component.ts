import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewTypeOfStock } from 'src/app/shared/models/typeOfStock';
import { TypeOfStockService } from '../type-of-stock.service';

@Component({
  selector: 'app-type-of-stock-add',
  templateUrl: './type-of-stock-add.component.html',
  styleUrls: ['./type-of-stock-add.component.scss']
})
export class TypeOfStockAddComponent implements OnInit {
  typeofstockForm: FormGroup;

  constructor(private typeofstockService: TypeOfStockService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createTypeofstockForm();
  }

  createTypeofstockForm() {
    this.typeofstockForm = this.fb.group({
      label: ['', [Validators.required]]
    });
  }

  onSubmit() {
    this.typeofstockService.createTypeOfStock(this.typeofstockForm.value).subscribe(() => {
      this.resetForm(this.typeofstockForm);
      this.router.navigateByUrl('typesofstock');
    },
    error => {
      console.log(error);
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.typeofstockService.formData = new INewTypeOfStock();
  }

}
