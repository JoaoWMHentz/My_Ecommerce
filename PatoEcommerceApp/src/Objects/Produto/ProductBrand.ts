import { ProductCategory } from "./ProductCategory"

export class ProductBrand {

    constructor(brandId: number, category: ProductCategory, name: string, description: string){
        this.BrandId = brandId;
        this.Category = category;
        this.Name = name;
        this.Description = description;
    } 

    BrandId: number = 0;

    Category: ProductCategory = new ProductCategory;

    Name: string = "";

    Description: string = "";
}