import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(private modalService: NgbModal) {}
  error = false;
  ngOnInit(): void {}

  openRegister() {
    this.modalService.dismissAll();
    this.modalService.open(RegisterComponent, {
      centered: true,
      size: 'xl',
    });
  }
}
