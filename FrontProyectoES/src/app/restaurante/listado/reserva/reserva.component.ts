import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Reserva } from 'src/app/shared/Models/reserva.model';
import { Restaurante } from 'src/app/shared/Models/restaurante.model';
import { ReservaService } from 'src/app/shared/services/ReservaService/reserva.service';

@Component({
  selector: 'app-reserva',
  templateUrl: './reserva.component.html',
  styleUrls: ['./reserva.component.css'],
})
export class ReservaComponent implements OnInit {
  @Input() restaurante: Restaurante;
  constructor(private formBuilder: FormBuilder, private modalService: NgbModal, private reservaService: ReservaService) {}
  reservaForm: FormGroup;
  ngOnInit(): void {
    this.reservaForm = this.formBuilder.group({
      comensales: ['', Validators.required, Validators.minLength(1)],
      fecha: ['', Validators.required],
      comentario: [''],
    });
  }

  reservar() {
    console.log(this.reservaForm.value);
    let reserva: Reserva = {
      personas: this.reservaForm.value.comensales,
      instruccionesEspeciales: this.reservaForm.value.comentario,
      restaurante: this.restaurante,
      usuario: JSON.parse(localStorage.getItem('usuario')!),
      servicios: this.restaurante.servicio,
    };
    this.reservaService.reservar(reserva).subscribe(res => {
      console.log(res);
      this.modalService.dismissAll();
    });
  }
}
