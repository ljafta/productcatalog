export interface Product {
  id: string;
  name: string;
  description: string;
  sku: string;
  price: number;
  quantity: number;
  categoryId: string;
}
export interface CreateProductDto {
  name: string;
  description?: string;
  sku: string;
  price: number;
  quantity: number;
  categoryId: string; // GUID as string
}

export interface UpdateProductDto {
  name: string;
  description?: string;
  sku: string;
  price: number;
  quantity: number;
  categoryId: string;
}

