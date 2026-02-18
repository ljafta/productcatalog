export interface Category {
  id: string;
  name: string;
  description: string;
  parentCategoryId?: string;
}

export interface CreateCategoryDto {
  name: string;
  description?: string;
  parentCategoryId?: string | null;
}