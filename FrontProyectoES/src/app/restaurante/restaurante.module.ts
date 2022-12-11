import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModalModule, NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';

import { DetalleComponent } from './detalle/detalle.component';
import { ListadoComponent } from './listado/listado.component';
import { ReservaComponent } from './listado/reserva/reserva.component';
import { RestauranteRoutingModule } from './restaurante-routing.module';

@NgModule({
  declarations: [ListadoComponent, DetalleComponent, ReservaComponent],
  imports: [CommonModule, RestauranteRoutingModule, NgbPopoverModule, NgbModalModule, ReactiveFormsModule],
})
export class RestauranteModule {}
