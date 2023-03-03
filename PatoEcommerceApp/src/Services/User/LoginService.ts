import { UserLogin } from 'src/Objects/User/UserLogin';
import { Service } from '../Service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from 'src/Objects/Produto/Product';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  pathBase: string = "/Api/Login";

  constructor(private http: HttpClient) { }

  public GetProduct(login: UserLogin):Observable<string> {
    try {
        return this.http.post<string>(Service.url + this.pathBase, login);
    } catch (error) {
        if (error. === 404) {
    }
    
  }
}