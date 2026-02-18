import { Routes } from '@angular/router';
import { ProductListComponent } from './features/products/product-list/product-list.component';
import { AddProductComponent } from './features/products/add-product/add-product.component';
import { CategoryListComponent } from './features/categories/category-list/category-list.component';
import { AddCategoryComponent } from './features/categories/add-category/add-category.component';
import { ProductFormComponent } from './features/products/product-form/product-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },

  { path: 'products', component: ProductListComponent },
  { path: 'products/add', component: AddProductComponent },
  { path: 'products/edit/:id', component: ProductFormComponent },

  { path: 'categories', component: CategoryListComponent },
  { path: 'categories/add', component: AddCategoryComponent },

  { path: '**', redirectTo: 'products' }
];


