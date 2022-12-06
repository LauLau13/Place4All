import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from 'src/app/home/login/login.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  constructor(private modalService: NgbModal) {}

  ngOnInit(): void {}

  goToLogin() {
    const modalRef = this.modalService.open(LoginComponent);
  }
}
