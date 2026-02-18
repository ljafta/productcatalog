import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../../core/services/category';
import { Category } from '../../../core/models/category.model';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './category-list.component.html'
})
export class CategoryListComponent {
  private categoryService = inject(CategoryService);
  categories: Category[] = [];

  ngOnInit() {
    this.categoryService.getAll()
      .subscribe(c => (this.categories = c));
      
  }
}
