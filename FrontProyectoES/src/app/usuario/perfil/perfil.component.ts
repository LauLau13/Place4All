import { Component, OnInit } from '@angular/core';
import { Reserva } from 'src/app/shared/Models/reserva.model';
import { Usuario } from 'src/app/shared/Models/usuario.model';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css'],
})
export class PerfilComponent implements OnInit {
  usuario: Usuario;
  reservas: Reserva[];
  ngOnInit(): void {
    let usuario = JSON.parse(localStorage.getItem('usuario')!);
    if (usuario) {
      this.usuario = usuario;
    }
  }
}
