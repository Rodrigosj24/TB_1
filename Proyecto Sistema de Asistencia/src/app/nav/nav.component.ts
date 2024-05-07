import { Component, Inject, OnInit } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { Router, RouterOutlet } from '@angular/router';
import { DomService } from '../dom.service';
import { CommonModule, DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    RouterOutlet,
    CommonModule,
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit {
  nombreUsuario = '';
  administrar = true;
  usuario: any = null;
  imagenBase64: string | ArrayBuffer | null = null;
  constructor(
    private router: Router,
    private domService: DomService,
    @Inject(DOCUMENT) private docume: Document
  ) {}
  ngOnInit(): void {
    const sessionStorage: any = this.docume.defaultView?.sessionStorage;

    if (sessionStorage) {
      this.usuario = JSON.parse(sessionStorage.getItem('miDato'));
      if (this.usuario) {
        this.nombreUsuario = this.usuario.nombres_apellidos;
        this.imagenBase64 = this.usuario.imagen;
        if (this.usuario.id_rol == 1) {
          this.administrar = true;
        } else {
          this.administrar = false;
        }
      }
    }
  }
  marcarAsistencia() {
    this.router.navigateByUrl('/home/marcar-asistencia');
  }
  crearusuario() {
    this.router.navigateByUrl('/home/usuario');
  }
  listausuario() {
    this.router.navigateByUrl('/home/listar-usuario');
  }
  cerrarSesion() {
    this.router.navigateByUrl('/login');
    this.domService.setLogin(true);
  }
}
