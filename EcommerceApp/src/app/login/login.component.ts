import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../Services/User/LoginService';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppComponent } from '../app.component';
import { UserLogin } from 'src/Objects/User/UserLogin';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formulario: FormGroup;
  constructor(private formB: FormBuilder ,private service: LoginService) {
    this.formulario = this.formB.group({
      UserId: ['', Validators.required],
      Password: ['', Validators.required]
    })
  }
  ngOnInit(): void {
   
  }

  onSubmit(){
    console.log(this.formulario.value);
    let login: UserLogin = {...this.formulario.value}
    this.service.Login(login).subscribe(products => { console.log(products) })
  }

}
