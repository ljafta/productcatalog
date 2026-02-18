import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CategoryListComponent } from './category-list.component';
import { CommonModule } from '@angular/common';

describe('CategoryListComponent', () => {
  let component: CategoryListComponent;
  let fixture: ComponentFixture<CategoryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonModule, CategoryListComponent], // standalone component
    }).compileComponents();

    fixture = TestBed.createComponent(CategoryListComponent);
    component = fixture.componentInstance;

    // Mock categories data
    component.categories = [
      { id: 1, name: 'Electronics', parentCategoryId: null },
      { id: 2, name: 'Phones', parentCategoryId: 1 },
      { id: 3, name: 'Laptops', parentCategoryId: 1 },
    ];

    fixture.detectChanges(); // trigger template rendering
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should display parent category name correctly', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    const parentCells = compiled.querySelectorAll('tbody tr td:nth-child(2)');
    
    // Electronics has no parent
    expect(parentCells[0].textContent?.trim()).toBe('—');

    // Phones parent is Electronics
    expect(parentCells[1].textContent?.trim()).toBe('Electronics');

    // Laptops parent is Electronics
    expect(parentCells[2].textContent?.trim()).toBe('Electronics');
  });
});

