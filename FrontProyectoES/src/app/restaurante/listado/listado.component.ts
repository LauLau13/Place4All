import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Restaurante } from 'src/app/shared/Models/restaurante.model';
import { RestauranteService } from 'src/app/shared/services/restauranteServicio/restaurante.service';
import { ReservaComponent } from './reserva/reserva.component';

@Component({
  selector: 'app-listado',
  templateUrl: './listado.component.html',
  styleUrls: ['./listado.component.css'],
})
export class ListadoComponent implements OnInit {
  routeSubs: Subscription;
  ciudad: string;
  restaurantes: Restaurante[];
  constructor(
    private route: ActivatedRoute,
    private restauranteService: RestauranteService,
    private router: Router,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.routeSubs = this.route.params.subscribe(params => {
      this.ciudad = params['ciudad'];
      if (this.ciudad) {
        this.busquedaCiudad();
        return;
      }
      this.getCiudades();
    });
  }

  busquedaCiudad() {
    let body = {
      ciudad: this.ciudad,
    };
    this.restauranteService.getRestauranteByCiudad(JSON.stringify(body)).subscribe((res: any) => {
      this.restaurantes = res;
    });
  }
  getCiudades() {
    this.restauranteService.getRestaurantes().subscribe((res: any) => {
      this.restaurantes = res;
    });
  }
  goToRestaurante(id: string) {
    this.router.navigate(['/restaurantes', id]);
  }
  reservar(restaurante: Restaurante) {
    let modal = this.modalService.open(ReservaComponent);
    modal.componentInstance.restaurante = restaurante;
  }
}
