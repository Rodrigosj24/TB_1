import { Component, OnInit, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';

import { ModalErrorComponent } from '../modal-error/modal-error.component';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { ModalExitosoComponent } from '../modal-exitoso/modal-exitoso.component';
@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css',
})
export class UsuarioComponent implements OnInit {
  mostrarContrasena: boolean = false;
  mostrarContrasena1: boolean = false;
  selectedImage: any;
  formUsuario: FormGroup;
  emailInvalid = true;
  image: any = '';
  @ViewChild('fileInput') fileInput: any;
  roles: any = [];
  registroExitoso = false;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    public dialog: MatDialog,
    private apiService: ApiService
  ) {
    this.formUsuario = this.fb.group({
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      email: ['', Validators.required],
      celular: ['', Validators.required],
      password: ['', Validators.required],
      password1: ['', Validators.required],
      permisos: ['', Validators.required],
      image: [null, Validators.nullValidator],
    });
  }

  ngOnInit(): void {
    this.apiService.getRoles().subscribe({
      next: (resp) => {
        this.roles = resp.data;
      },
      error: () => {
        // this.openErrorDialog(
        //   '0ms',
        //   '0ms',
        //   'No se pudo completar la solicitud. Verifica tu conexión.'
        // );
        // return;
      },
    });
  }

  guardar() {
    if (this.formUsuario.valid) {
      const email = this.formUsuario.controls['email'].value;

      const password = this.formUsuario.controls['password'].value;
      const password1 = this.formUsuario.controls['password1'].value;
      const celular = this.formUsuario.controls['celular'].value;

      const nombres = this.formUsuario.controls['nombre'].value;
      const apellidos = this.formUsuario.controls['apellido'].value;
      const permisos = this.formUsuario.controls['permisos'].value;
      // Expresión regular para validar un email
      const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

      if (!emailPattern.test(email)) {
        this.openErrorDialog(
          '0ms',
          '0ms',
          'Por favor, Completa un correo válido.'
        );
        return;
      }
      if (password != password1) {
        this.openErrorDialog('0ms', '0ms', 'Las contraseñas no coinciden.');
        return;
      }
      // Expresión regular para validar un número de celular con solo números
      const celularPattern = /^\d+$/;

      if (!celularPattern.test(celular)) {
        this.openErrorDialog(
          '0ms',
          '0ms',
          'Por favor, ingresa un número de celular válido.'
        );
        return;
      }

      this.apiService
        .registrarUsuario({
          nombres: nombres,
          apellidos: apellidos,
          celular: celular,
          correo: email,
          contrasenia: password,
          id_rol: permisos,
          imagen: this.image,
        })
        .subscribe({
          next: (resp) => {
            this.mostrarMensajeExito();
            // this.limpiar();
          },
          error: (err) => {
            if (err.error.message) {
              this.openErrorDialog('0ms', '0ms', err.error.message);
            }
            // this.openErrorDialog(
            //   '0ms',
            //   '0ms',
            //   'No se pudo completar la solicitud. Verifica tu conexión.'
            // );
            // return;
          },
        });
    } else {
      this.openErrorDialog(
        '0ms',
        '0ms',
        'Por favor, Completa todos los campos.'
      );
      return;
    }

    // this.router.navigateByUrl('/home');
  }
  limpiar() {
    this.formUsuario.markAsUntouched(); // Marca el formulario como "untouched"
    this.formUsuario.reset(); // Restablece el formulario

    this.selectedImage = null; // Limpia la imagen seleccionada
  }
  async onFileSelected(event: any) {
    const file: File = event.target.files[0];
    this.image = await this.convertirImagenABase64(file);

    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.selectedImage = reader.result;
    };
  }
  convertirImagenABase64(file: File): Promise<string | ArrayBuffer | null> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => {
        resolve(reader.result);
      };
      reader.onerror = reject;
      reader.readAsDataURL(file);
    });
  }
  displayImage(file: File) {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.selectedImage = reader.result;
    };
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
  
  mostrarMensajeExito() {
    const dialogRef = this.dialog.open(ModalExitosoComponent, {
      width: '400px', // Ancho del modal
    });

    // Opcional: Manejar acciones después de cerrar el modal
    dialogRef.afterClosed().subscribe(() => {
      this.limpiar();
      // Puedes realizar acciones adicionales después de cerrar el modal
    });
  }

}
