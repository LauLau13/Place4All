import { Component, OnInit } from '@angular/core';
import { Reserva } from 'src/app/shared/Models/reserva.model';
import { Usuario } from 'src/app/shared/Models/usuario.model';
import { ReservaService } from 'src/app/shared/services/ReservaService/reserva.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css'],
})
export class PerfilComponent implements OnInit {
  constructor(private reservaService: ReservaService) {}
  usuario: Usuario;
  reservas: Reserva[];
  ngOnInit(): void {
    let usuario = JSON.parse(localStorage.getItem('usuario')!);
    if (usuario) {
      this.usuario = usuario;
      this.getReservas();
    }
  }
  getReservas() {
    this.reservaService.getReservasUsuario(this.usuario.id!).subscribe((res: any) => {
      this.reservas = res;
    });
  }
}
