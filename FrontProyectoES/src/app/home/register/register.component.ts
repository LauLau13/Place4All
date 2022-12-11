import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Usuario } from 'src/app/shared/Models/usuario.model';
import { LoginService } from 'src/app/shared/services/LoginService/login.service';
import { UsuarioService } from 'src/app/shared/services/UsuarioServicio/usuario.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerGroup!: FormGroup;
  addressGroup!: FormGroup;

  passwordMatchValidator(form: FormGroup) {
    // Obtiene los valores de los controles password y confirmPassword
    const password = form.get('password')!.value;
    const confirmPassword = form.get('confirmPassword')!.value;

    // Si los valores son distintos, devuelve un objeto con el mensaje de error
    if (password !== confirmPassword) {
      return {
        passwordMatch: 'Las contraseñas no coinciden',
      };
    }

    // Si los valores son iguales, devuelve null
    return null;
  }

  constructor(
    private formBuilder: FormBuilder,
    private usuarioService: UsuarioService,
    private modalService: NgbModal,
    private loginService: LoginService
  ) {}

  ngOnInit(): void {
    this.registerGroup = this.formBuilder.group(
      {
        // Define los controles y las validaciones que deseas utilizar en el formulario
        nombre: ['', [Validators.required, Validators.minLength(3)]],
        apellido: ['', [Validators.required, Validators.minLength(3)]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(3)]],
        confirmPassword: ['', [Validators.required, Validators.minLength(3)]],
        gradoDiscapacidad: ['No', [Validators.required]],
        genero: ['', [Validators.required]],
        edad: ['', Validators.required],
        tieneDiscapacidad: ['', Validators.required],
      },
      { Validators: this.passwordMatchValidator }
    );

    this.addressGroup = this.formBuilder.group({
      calle: ['', [Validators.required, Validators.minLength(3)]],
      numero: ['', [Validators.required, Validators.minLength(1)]],
      ciudad: ['', [Validators.required, Validators.minLength(3)]],
      provincia: ['', [Validators.required, Validators.minLength(3)]],
      cp: ['', [Validators.required, Validators.minLength(3)]],
    });
  }

  onSubmit() {
    if (this.registerGroup.valid && this.addressGroup.valid) {
      const nombre = this.registerGroup.get('nombre')?.value;
      const apellido = this.registerGroup.get('apellido')?.value;
      const email = this.registerGroup.get('email')?.value;
      const password = this.registerGroup.get('password')?.value;
      const gradoDiscapacidad = this.registerGroup.get('gradoDiscapacidad')?.value;
      const gender = this.registerGroup.get('genero')?.value;
      const edad = this.registerGroup.get('edad')?.value;
      const tieneDiscapacidad = this.registerGroup.get('tieneDiscapacidad')?.value;
      const calle = this.addressGroup.get('calle')?.value;
      const numero = this.addressGroup.get('numero')?.value;
      const ciudad = this.addressGroup.get('ciudad')?.value;
      const cp = this.addressGroup.get('cp')?.value;
      const provincia = this.addressGroup.get('provincia')?.value;

      let newUser: Usuario = {
        nombre: nombre,
        apellido: apellido,
        email: email,
        password: password,
        gradoDiscapacidad: gradoDiscapacidad,
        genero: gender,
        direccion: {
          calle: calle,
          numero: numero,
          ciudad: ciudad,
          cp: cp,
          provincia: provincia,
        },
        edad: edad,
        tieneDiscapacidad: tieneDiscapacidad,
      };

      this.usuarioService.createUser(newUser).subscribe(async res => {
        let jsonRes = Object.create(res);

        await this.loginService.login({ email: jsonRes.email, password: jsonRes.password });
      });

      this.modalService.dismissAll();
    }
  }
}
