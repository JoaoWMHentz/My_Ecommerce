import { ProductCategory } from "./ProductCategory"

export interface ProductBrand {
    brandId: number;

    category: ProductCategory;

    name: string;

    description: string;
}