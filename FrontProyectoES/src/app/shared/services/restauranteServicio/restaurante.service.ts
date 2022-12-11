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
    return this.http.get(`${api}/Restaurante`);
  }

  getRestaurante(id: string) {
    return this.http.get(`${api}/Restaurante/${id}`);
  }

  getRestauranteByCiudad(body: any) {
    return this.http.post(`${api}/Restaurante/search`, body);
  }
}
