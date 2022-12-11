import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(private router: Router) {}
  searchField: string = '';
  ngOnInit(): void {}
  onSearch() {
    this.router.navigate(['/restaurantes', { ciuidad: this.searchField }]);
  }
}
