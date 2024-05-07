import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router'; // Importación correcta
import { DomService } from '../dom.service';
import { ApiService } from '../api.service';

import { MatDialog } from '@angular/material/dialog';

import { ModalErrorComponent } from '../modal-error/modal-error.component';
// import { HttpClientModule } from '@angular/common/http';
// import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  formLogin: FormGroup;
  emailInvalid = true;
  mostrarContrasena = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private domService: DomService,
    private apiService: ApiService,
    public dialog: MatDialog
  ) {
    this.formLogin = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {}
  login() {
    this.formLogin.controls['email'].validator;
  
    if (this.formLogin.valid) {
      const email = this.formLogin.controls['email'].value;
      const password = this.formLogin.controls['password'].value;

      // Expresión regular para validar un email
      const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

      if (emailPattern.test(email)) {
  

        this.apiService
          .loginuser({ correo: email, contrasenia: password })
          .subscribe({
            next: (resp) => {
         
              this.domService.setDataUsuario(resp.data);
              this.router.navigateByUrl('/home');
              // Datos que quieres guardar
              sessionStorage.setItem('miDato', JSON.stringify(resp.data)); // Guardar los datos en sessionStorage
            },
            error: (error) => {
       
              if (error.error.message) {
                this.openErrorDialog('0ms', '0ms', error.error.message);
              }

              return;
            },
          });
      } else {
        this.openErrorDialog(
          '0ms',
          '0ms',
          'Por favor, escribe un correo válido.'
        );
        return;
      }
    } else {
      this.openErrorDialog(
        '0ms',
        '0ms',
        'Por favor, completa todos los campos.'
      );
      return;
    }

  }
  openErrorDialog(
    enterAnimationDuration: string,
    exitAnimationDuration: string,
    informacion: any
  ): void {
    this.dialog.open(ModalErrorComponent, {
      width: '400px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: informacion,
    });
  }
}
