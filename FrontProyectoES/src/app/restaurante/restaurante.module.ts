import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { DetalleComponent } from './detalle/detalle.component';
import { RestauranteRoutingModule } from './restaurante-routing.module';

@NgModule({
  declarations: [DetalleComponent],
  imports: [CommonModule, RestauranteRoutingModule],
})
export class RestauranteModule {}
