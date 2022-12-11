import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

const api = environment.api;
@Injectable({
  providedIn: 'root',
})
export class RestauranteService {
  constructor(private http: HttpClient) {}

  getRestaurantes() {
    return this.http.get(`${api}/restaurante`);
  }

  getRestaurante(id: string) {
    return this.http.get(`${api}/restaurante/${id}`);
  }

  getRestauranteByCategoria(categoria: string) {
    return this.http.get(`${api}/restaurante/categoria/${categoria}`);
  }
}
