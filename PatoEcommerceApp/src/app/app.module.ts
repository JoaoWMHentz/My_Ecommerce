import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CategoriaComponent } from './categoria/categoria.component';
import { ProdutoModelComponent } from './produto-model/produto-model.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
@NgModule({
  declarations: [
    AppComponent,

    CategoriaComponent,
    ProdutoModelComponent,
    LoginComponent,
    ProductDetailComponent

  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,


  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
