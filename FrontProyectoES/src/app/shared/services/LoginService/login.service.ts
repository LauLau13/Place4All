import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Login } from '../../Models/login.model';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  api = environment.api;
  constructor(private http: HttpClient) {}

  async login(body: Login): Promise<boolean> {
    fetch('https://localhost:7157/usuario/login', {
      method: 'POST',
      headers: {},
      body: JSON.stringify(body),
    })
      .then(response => {
        localStorage.setItem('token', JSON.stringify(response));
        return true;
      })
      .catch(err => {
        console.log(err);
        return false;
      });

    return false;
  }

  logOut() {}
}
