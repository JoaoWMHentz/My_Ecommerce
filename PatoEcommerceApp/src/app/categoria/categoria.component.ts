import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-categoria',
  templateUrl: './categoria.component.html',
  styleUrls: ['./categoria.component.css']
})
export class CategoriaComponent implements OnInit {
  constructor() { }
  @Input() name: string | undefined;
  @Input() img: string | undefined;
  
  ngOnInit(): void {
    
  }

}
