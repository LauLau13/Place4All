import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Reserva } from '../../Models/reserva.model';
const api = environment.api;
@Injectable({
  providedIn: 'root',
})
export class ReservaService {
  constructor(private http: HttpClient) {}

  reservar(reserva: Reserva) {
    return this.http.post(`${api}/Reserva`, reserva);
  }

  getReservas() {
    return this.http.get(`${api}/Reserva`);
  }

  getReservasUsuario(id: string) {
    return this.http.get(`${api}/Reserva/usuario/${id}`);
  }
}
