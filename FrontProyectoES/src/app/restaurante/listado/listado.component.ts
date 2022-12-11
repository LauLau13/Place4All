import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Restaurante } from 'src/app/shared/Models/restaurante.model';
import { RestauranteService } from 'src/app/shared/services/restauranteServicio/restaurante.service';

@Component({
  selector: 'app-listado',
  templateUrl: './listado.component.html',
  styleUrls: ['./listado.component.css'],
})
export class ListadoComponent implements OnInit {
  routeSubs: Subscription;
  ciudad: string;
  restaurantes: Restaurante[];
  constructor(private route: ActivatedRoute, private restauranteService: RestauranteService) {}

  ngOnInit(): void {
    this.routeSubs = this.route.params.subscribe(params => {
      console.log(params);
      this.ciudad = params['ciudad'];
      if (this.ciudad) {
        console.log(this.ciudad);
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
      console.log(res);
      this.restaurantes = res;
    });
  }
  getCiudades() {
    this.restauranteService.getRestaurantes().subscribe((res: any) => {
      this.restaurantes = res;
    });
  }
}
