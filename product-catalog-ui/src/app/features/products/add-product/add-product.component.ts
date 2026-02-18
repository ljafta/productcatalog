import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { ProductService } from '../../../core/services/product';
import { CategoryService } from '../../../core/services/category';

import { Category } from '../../../core/models/category.model';
import { CreateProductDto } from '../../../core/models/product.model';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-product.component.html',
    styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private router = inject(Router);

  categories: Category[] = [];

  model: CreateProductDto = {
    name: '',
    description: '',
    sku: '',
    price: 0,
    quantity: 0,
    categoryId: ''
  };

  saving = false;
  error?: string;

  ngOnInit() {
    this.categoryService.getAll()
      .subscribe(c => (this.categories = c));
  }

  save() {
    if (!this.model.categoryId) {
      this.error = 'Category is required';
      return;
    }

    this.saving = true;
    this.error = undefined;

    this.productService.create(this.model).subscribe({
      next: () => this.router.navigate(['/products']),
      error: err => {
        this.error = err.error ?? 'Failed to create product';
        this.saving = false;
      }
    });
  }
}