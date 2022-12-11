import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerGroup: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    disability: new FormControl('No'),
    gender: new FormControl(''),
  });

  addressGroup: FormGroup = new FormGroup({
    street: new FormGroup(''),
    number: new FormControl(''),
    city: new FormControl(''),
    province: new FormControl(''),
    zipCode: new FormControl(''),
  });

  constructor() {}

  ngOnInit(): void {}
}
