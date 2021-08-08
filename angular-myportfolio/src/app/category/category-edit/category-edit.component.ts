import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit {
  categoryForm: FormGroup;
  id: number;

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private categoryService: CategoryService
) { }

ngOnInit(): void {
  this.id = this.activatedRoute.snapshot.params['id'];

  this.categoryForm = this.formBuilder.group({
    id: [this.id],
    categoryName: new FormControl('', [Validators.required])
    });

  this.categoryService.getCategoryById(this.id)
  .pipe(first())
  .subscribe(x => this.categoryForm.patchValue(x));
}

onSubmit() {
  if (this.categoryForm.invalid) {
      return;
  }
  this.updateCategory();
}

private updateCategory() {
this.categoryService.updateCategory(this.id, this.categoryForm.value)
    .pipe(first())
    .subscribe(() => {
        this.router.navigateByUrl('categories');
      }, error => {
        console.log(error);
      });
    }
}



