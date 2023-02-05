export class ProductCategory {

  constructor(categoryId: number, name: string, image: string, subCat: boolean){
    this.CategoryId = categoryId;
    this.Name = name;
    this.Image = image;
    this.SubCat = subCat;
  }
  CategoryId: number = 0;
  Name: string = "";
  Image: string = "";
  SubCat: boolean = false;
}
