import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RestauranteRoutingModule } from './restaurante-routing.module';
import { ListadoComponent } from './listado/listado.component';

@NgModule({
  declarations: [ListadoComponent],
  imports: [CommonModule, RestauranteRoutingModule],
})
export class RestauranteModule {}
