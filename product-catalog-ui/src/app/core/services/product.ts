import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateProductDto, Product, UpdateProductDto } from '../models/product.model';
import { PagedResponse } from '../models/paged-response.model';
import { environment } from '../../../environments/environment';


@Injectable({ providedIn: 'root' })
export class ProductService {
  // Use environment.apiUrl
  private baseUrl = `${environment.apiUrl}/products`;

  constructor(private http: HttpClient) {}

  getAll(params: {
    page: number;
    pageSize: number;
    categoryId?: string;
    search?: string;
  }) {
    return this.http.get<PagedResponse<Product>>(this.baseUrl, {
      params: {
        page: params.page.toString(),
        pageSize: params.pageSize.toString(),
        ...(params.categoryId && { categoryId: params.categoryId }),
        ...(params.search && { search: params.search })
      }
    });
  }

  getById(id: string) {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }

  create(data: CreateProductDto) {
    return this.http.post<Product>(this.baseUrl, data);
  }

  update(id: string, dto: UpdateProductDto) {
  return this.http.put(`${this.baseUrl}/${id}`, dto);
}

  delete(id: string) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
