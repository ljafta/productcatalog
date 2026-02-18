import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from '../../../core/services/category';
import { Category, CreateCategoryDto } from '../../../core/models/category.model';


@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {
  private categoryService = inject(CategoryService);
  private router = inject(Router);

  categories: Category[] = [];

  model: CreateCategoryDto = {
    name: '',
    description: '',
    parentCategoryId: null
  };

  saving = false;
  error?: string;

  ngOnInit() {
    this.categoryService.getAll()
      .subscribe(c => (this.categories = c));
  }

  save() {
    if (!this.model.name.trim()) {
      this.error = 'Category name is required';
      return;
    }

    this.saving = true;
    this.error = undefined;

    this.categoryService.create(this.model).subscribe({
      next: () => {
        // Navigate to categories page **and reload categories**
        this.router.navigate(['/products']).then(() => {
          // Force reload of component (optional if not using BehaviorSubject)
          window.location.reload();
        });
      },
      error: err => {
        this.error = err.error ?? 'Failed to create category';
        this.saving = false;
      }
    });
  }

}
