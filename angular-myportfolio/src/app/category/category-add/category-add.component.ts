import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewCategory } from 'src/app/shared/models/category';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.scss']
})
export class CategoryAddComponent implements OnInit {
  categoryForm: FormGroup;

  constructor(private categoryService: CategoryService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createCategoryForm();
  }

  createCategoryForm() {
    this.categoryForm = this.fb.group({
      categoryName: ['', [Validators.required]]
    });
  }

  onSubmit() {
    this.categoryService.createCategory(this.categoryForm.value).subscribe(() => {
      this.resetForm(this.categoryForm);
      this.router.navigateByUrl('categories');
    },
    error => {
      console.log(error);
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.categoryService.formData = new INewCategory();
  }

}
