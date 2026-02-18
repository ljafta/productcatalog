import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

import { ProductService } from '../../../core/services/product';
import { CategoryService } from '../../../core/services/category';
import { Product } from '../../../core/models/product.model';
import { Category } from '../../../core/models/category.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-list.component.html',
  styleUrls: ['product-list.component.css']
})
export class ProductListComponent {
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);

  products: Product[] = [];
  categories: Category[] = [];

  hasCategories = false;

  page = 1;
  pageSize = 10;
  total = 0;

  search = '';
  categoryId: string | null = null;

  loading = false;

  ngOnInit() {
    this.loadCategories();
    this.loadProducts();
    
  }

  loadCategories() {
    this.categoryService.getAll().subscribe(cats => {
      this.categories = cats;
      this.hasCategories = cats.length > 0;
    });
  }

  loadProducts() {
    this.loading = true;

    this.productService.getAll({
      page: this.page,
      pageSize: this.pageSize,
      categoryId: this.categoryId || undefined,
      search: this.search || undefined
    }).subscribe(res => {
      this.products = res.items;
      this.total = res.totalCount;
      this.loading = false;
    });
  }

  onFilterChange() {
    this.page = 1;
    this.loadProducts();
  }

  next() {
    if (this.page * this.pageSize < this.total) {
      this.page++;
      this.loadProducts();
    }
  }

  prev() {
    if (this.page > 1) {
      this.page--;
      this.loadProducts();
    }
  }

  delete(id: string) {
    if (!confirm('Delete product?')) return;
    this.productService.delete(id).subscribe(() => this.loadProducts());
  }

  get totalPages(): number {
    return Math.ceil(this.total / this.pageSize) || 1;
  }
}



