import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category, CreateCategoryDto } from '../models/category.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class CategoryService {
 private baseUrl = `${environment.apiUrl}/categories`;


  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Category[]>(this.baseUrl);
  }

   create(dto: CreateCategoryDto) {
    return this.http.post(this.baseUrl, dto);
  }
}

export type { Category };

