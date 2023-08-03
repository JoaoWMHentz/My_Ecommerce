import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProdutoModelComponent } from './produto-model/produto-model.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'Products', component: ProdutoModelComponent},
  {path: '', component: ProdutoModelComponent},
  {path: 'ProductDetails', component: ProductDetailComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
