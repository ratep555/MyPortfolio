import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { TypeOfStockService } from '../type-of-stock.service';

@Component({
  selector: 'app-type-of-stock-edit',
  templateUrl: './type-of-stock-edit.component.html',
  styleUrls: ['./type-of-stock-edit.component.scss']
})
export class TypeOfStockEditComponent implements OnInit {
  typeofstockForm: FormGroup;
  id: number;

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private typeofstockService: TypeOfStockService
) { }

ngOnInit(): void {
  this.id = this.activatedRoute.snapshot.params['id'];

  this.typeofstockForm = this.formBuilder.group({
    id: [this.id],
    label: ['', [Validators.required]]
    });

  this.typeofstockService.getTypeOfStockById(this.id)
  .pipe(first())
  .subscribe(x => this.typeofstockForm.patchValue(x));
}

onSubmit() {
  if (this.typeofstockForm.invalid) {
      return;
  }
  this.updateTypeofstock();
}

private updateTypeofstock() {
this.typeofstockService.updateTypeOfStock(this.id, this.typeofstockForm.value)
    .pipe(first())
    .subscribe(() => {
        this.router.navigateByUrl('typesofstock');
      }, error => {
        console.log(error);
      });
    }
}
