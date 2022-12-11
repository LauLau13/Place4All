import { Usuario } from './usuario.model';

export interface Login {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  usuario: Usuario;
}
