import { Direccion } from './direccion.model';
import { Servicio } from './servicio.model';

export interface Restaurantes {
  restaurantes: Restaurante[];
}

export interface Restaurante {
  id: string;
  nombre: string;
  direccion: Direccion;
  descripcion: string;
  telefono: string;
  imagen: string;
  servicio: Servicio[];
}
