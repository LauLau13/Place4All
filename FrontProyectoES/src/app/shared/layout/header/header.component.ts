import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from 'src/app/home/login/login.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  constructor(private modalService: NgbModal, private router: Router) {}

  ngOnInit(): void {}

  goToLogin() {
    if (localStorage.getItem('usuario') !== null && localStorage.getItem('usuario') !== undefined) {
      this.router.navigate(['/perfil']);
      return;
    }
    this.modalService.open(LoginComponent);
  }
}
