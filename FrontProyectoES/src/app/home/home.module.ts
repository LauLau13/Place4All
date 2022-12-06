import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [HomeComponent, LoginComponent],
  imports: [CommonModule, HomeRoutingModule, NgbAlertModule],
})
export class HomeModule {}
