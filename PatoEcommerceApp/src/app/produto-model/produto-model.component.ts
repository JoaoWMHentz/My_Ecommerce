import { Component, OnInit } from '@angular/core';
import { Product } from 'src/Objects/Produto/Product';
import {ProductService} from '../../Services/Product/ProductService';
@Component({
  selector: 'app-produto-model',
  templateUrl: './produto-model.component.html',
  styleUrls: ['./produto-model.component.css']
})
export class ProdutoModelComponent implements OnInit {

  source: Array<Product> = [];
  constructor( private service: ProductService) 
  { 
    this.service.GetProduct().subscribe(products => { this.source = products; console.log(products) })
  }

  ngOnInit(): void {
    
  }

}
