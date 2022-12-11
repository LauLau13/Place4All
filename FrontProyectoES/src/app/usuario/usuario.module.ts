import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsuarioRoutingModule } from './usuario-routing.module';
import { PerfilComponent } from './perfil/perfil.component';

@NgModule({
  declarations: [PerfilComponent],
  imports: [CommonModule, UsuarioRoutingModule],
})
export class UsuarioModule {}
