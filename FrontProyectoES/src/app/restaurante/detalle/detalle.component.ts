import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Restaurante } from 'src/app/shared/Models/restaurante.model';
import { RestauranteService } from 'src/app/shared/services/restauranteServicio/restaurante.service';

@Component({
  selector: 'app-detalle',
  templateUrl: './detalle.component.html',
  styleUrls: ['./detalle.component.css'],
})
export class DetalleComponent implements OnInit, OnDestroy {
  routeSub: Subscription = new Subscription();
  restaurantId = '';
  restaurante: Restaurante;
  constructor(private route: ActivatedRoute, private restauranteService: RestauranteService) {}

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.restaurantId = params['id'];
    });

    if (this.restaurantId !== null && this.restaurantId !== undefined) {
      this.restauranteService.getRestaurante(this.restaurantId).subscribe((res: any) => {
        this.restaurante = res;
      });
    }
  }

  reservar() {}
  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }
}
