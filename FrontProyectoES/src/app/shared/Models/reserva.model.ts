import { Restaurante } from './restaurante.model';
import { Servicio } from './servicio.model';
import { Usuario } from './usuario.model';

export interface Reserva {
  id?: string;
  usuario: Usuario;
  restaurante: Restaurante;
  personas: number;
  instruccionesEspeciales: string;
  servicios: Servicio[];
}
