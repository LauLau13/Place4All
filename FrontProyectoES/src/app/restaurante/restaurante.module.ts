import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { DetalleComponent } from './detalle/detalle.component';
import { ListadoComponent } from './listado/listado.component';
import { RestauranteRoutingModule } from './restaurante-routing.module';

@NgModule({
  declarations: [ListadoComponent, DetalleComponent],
  imports: [CommonModule, RestauranteRoutingModule],
})
export class RestauranteModule {}
