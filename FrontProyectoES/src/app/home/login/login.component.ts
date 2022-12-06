import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Login } from 'src/app/shared/Models/login.model';
import { LoginService } from 'src/app/shared/services/LoginService/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(private loginService: LoginService) {}
  error = false;
  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });
  ngOnInit(): void {}

  async login() {
    var login: Login = {
      email: this.loginForm.value.email!,
      password: this.loginForm.value.password!,
    };

    console.log(login);
    debugger;
    const res = await this.loginService.login(login);
    if (!res) {
      this.error = true;
    }
  }
}
