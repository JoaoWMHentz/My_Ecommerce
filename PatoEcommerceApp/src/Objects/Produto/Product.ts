import { ProductImage } from "./ProductImage";
import { User } from "../User";
import { ProductCategory } from "./ProductCategory"
import { ProductUseState } from "./ProductUseState";
import { ProductBrand } from "./ProductBrand";
import { ProductType } from "./ProductType";

export class Product {

    constructor(productId: number, name: string, salesPerson: User, category: ProductCategory,
                brand: ProductBrand, type: ProductType, description: string, state: ProductUseState,  images: Array<ProductImage>)
    {
        this.ProductId = productId;
        this.Name = name;
        this.SalesPerson = salesPerson;
        this.Category = category;
        this.Brand = brand;
        this.Type = type;
        this.Description = description;
        this.State = state    
        this.Images = images;
    }

    ProductId: number

    Name: string

    SalesPerson: User = new User;

    Category: ProductCategory = new ProductCategory;

    Brand: ProductBrand;

    Type: ProductType = new ProductType;

    Description: string;

    State: ProductUseState = new ProductUseState;

    Images: Array<ProductImage>;
}