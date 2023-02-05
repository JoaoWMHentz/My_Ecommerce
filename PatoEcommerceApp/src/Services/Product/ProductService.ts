import { Service } from '../Service';
import { Product } from '../../Objects/Produto/Product';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  pathBase: string = "/api/produto/get";

  constructor(private http: HttpClient) { }

  public GetProduct():Observable<Product[]> {
    try {
        var Products = this.http.get(Service.url + this.pathBase);
    } catch (error) {
        throw error;
    }
    
  }
  public PostProduct(product: Product){
    try {
        return this.http.post(Service.url + this.pathBase, product);
    } catch (error) {
        throw error;
    }
  }
}
