import { Component, Input, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';


import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of } from 'rxjs';
import { ProductService } from '../../../core/services/product';
import { CategoryService } from '../../../core/services/category';
import { CreateProductDto, Product, UpdateProductDto } from '../../../core/models/product.model';
import { Category } from '../../../core/models/category.model';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-product-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  categories: Category[] = [];
  isEdit = false;
  productId?: string;

  form = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    sku: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(0.01)]],
    quantity: [0, [Validators.required, Validators.min(0)]],
    categoryId: ['', Validators.required]
  });

  ngOnInit() {
    this.categoryService.getAll().subscribe(c => this.categories = c);

    this.productId = this.route.snapshot.paramMap.get('id') ?? undefined;
    if (this.productId) {
      this.isEdit = true;
      this.productService.getById(this.productId)
        .subscribe(p => this.form.patchValue(p));
    }
  }

save() {
  if (this.form.invalid) return;

  const formValue = this.form.value;

  const payload: UpdateProductDto = {
    name: formValue.name!,
    description: formValue.description || '',
    sku: formValue.sku!,
    price: formValue.price!,
    quantity: formValue.quantity!,
    categoryId: formValue.categoryId!
  };

  const request$ = this.isEdit && this.productId
    ? this.productService.update(this.productId, payload)
    : this.productService.create(payload);

  request$.subscribe(() => this.router.navigate(['/products']));
}

}

