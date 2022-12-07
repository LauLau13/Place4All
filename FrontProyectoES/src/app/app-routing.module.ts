import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//TODO: BORRAR RUTA
const routes: Routes = [
  {
    path: '',
    /* loadChildren: () => import('./home/home.module').then(m => m.HomeModule), */

    loadChildren: () => import('./restaurante/restaurante.module').then(m => m.RestauranteModule),
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full',
  },
  {
    path: 'restaurante',
    loadChildren: () => import('./restaurante/restaurante.module').then(m => m.RestauranteModule),
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes, { enableTracing: true })],
})
export class AppRoutingModule {}
