import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Usuario } from '../../Models/usuario.model';

const api = environment.api;
@Injectable({
  providedIn: 'root',
})
export class UsuarioService {
  constructor(private http: HttpClient) {}

  getUser(id: string) {
    return this.http.get<Usuario>(`${api}/usuario/${id}`);
  }

  createUser(usuario: Usuario) {
    return this.http.post(`${api}/usuario`, usuario);
  }

  updateUser(usuario: Usuario) {
    return this.http.put(`${api}/usuario/${usuario.id}`, usuario);
  }

  deleteUser(id: string) {
    return this.http.delete(`${api}/usuario/${id}`);
  }
}
