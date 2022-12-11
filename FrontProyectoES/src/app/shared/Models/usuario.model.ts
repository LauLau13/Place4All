import { Direccion } from './direccion.model';

export interface Usuario {
  id?: string;
  nombre: string;
  apellido: string;
  genero: string;
  edad: number;
  direccion: Direccion;
  tieneDiscapacidad: boolean;
  gradoDiscapacidad: number;
  email: string;
  password: string;
}
