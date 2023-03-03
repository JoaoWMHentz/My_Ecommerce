import { ProductImage } from "./ProductImage";
import { User } from "../User/User";
import { ProductCategory } from "./ProductCategory"
import { ProductUseState } from "./ProductUseState";
import { ProductBrand } from "./ProductBrand";
import { ProductType } from "./ProductType";

export interface Product {
    productId: number

    name: string

    salesPerson: User;

    category: ProductCategory;

    brand: ProductBrand;

    type: ProductType;

    description: string;

    state: ProductUseState;

    images: Array<ProductImage>;

    price: number;

    amount: number;
}