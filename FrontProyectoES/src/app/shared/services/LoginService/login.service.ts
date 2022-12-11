import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Login, LoginResponse } from '../../Models/login.model';
import { Usuario } from '../../Models/usuario.model';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  api = environment.api;
  constructor(private http: HttpClient) {}

  async login(body: Login): Promise<LoginResponse> {
    /* return this.http.post<LoginResponse>(`${this.api}/Usuario/login`, JSON.stringify(body)); */

    const response = await fetch(`${this.api}/Usuario/login`, {
      method: 'POST',
      body: JSON.stringify(body),
      headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
      },
    });

    const result = await response.json();
    return result;
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  setUser(usuario: Usuario) {
    localStorage.setItem('usuario', JSON.stringify(usuario));
  }
  logOut() {}
}
