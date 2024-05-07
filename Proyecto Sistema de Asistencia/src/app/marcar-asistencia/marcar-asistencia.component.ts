import { Component, Inject, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe } from '@angular/common';
import { ApiService } from '../api.service';
import { DomService } from '../dom.service';
import { CommonModule, DOCUMENT } from '@angular/common';
@Component({
  selector: 'app-marcar-asistencia',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, DatePipe, CommonModule],
  templateUrl: './marcar-asistencia.component.html',
  styleUrl: './marcar-asistencia.component.css',
})
export class MarcarAsistenciaComponent implements OnInit {
  fecha: string;
  // temporizador
  timer: any = {
    minute: 0,
    second: 0,
    hour: 0,
  };
  final: any = {
    minute: 0,
    second: 0,
    hour: 4,
  };
  timerInterval: any;
  finalInterval: any;
  horaView: any = '00:00:00';
  horaFinal: any = '00:00:00';
  btnIniciar: boolean = true;
  btnFinalizar: boolean = false;
  usuario: any = {};
  constructor(
    private dom: DomService,
    private apiService: ApiService,
    @Inject(DOCUMENT) private docume: Document
  ) {
    this.fecha = this.obtenerFechaActual();
    const sessionStorage: any = docume.defaultView?.sessionStorage;

    if (sessionStorage) {
      this.usuario = JSON.parse(sessionStorage.getItem('miDato'));
      const fecha = JSON.parse(sessionStorage.getItem('fechaInicio'));
      const final = JSON.parse(sessionStorage.getItem('fechaFinal'));
      if (fecha && final) {
        this.timer = fecha;
        this.final = final;
        this.iniciar();
      }
    }
  }
  ngOnInit(): void {
    this.dom.obtLogin$.subscribe((data) => {
 

      if (data) {
        this.dom.setLogin(null);
        this.btnFinalizar = false;
        this.btnIniciar = true;
        clearInterval(this.timerInterval);
        clearInterval(this.finalInterval);
        sessionStorage.removeItem('miDato');
        sessionStorage.removeItem('fechaInicio');
        sessionStorage.removeItem('fechaFinal');
        this.horaView = '00:00:00';
        this.horaFinal = '00:00:00';
        this.timer = {
          minute: 0,
          second: 0,
          hour: 0,
        };
        this.final = {
          minute: 0,
          second: 0,
          hour: 4,
        };
      }
    });
  }
  // users() {
  //   const a: any = sessionStorage;
  //   let data: any = a.getItem('miDato');
  //   this.usuario = JSON.parse(data);
  // }

  obtenerFechaActual(): any {
    const today = new Date();
    const options: any = {
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    };
    return today.toLocaleDateString('es-ES', options);
  }

  reloj(tiempo: any) {
    this.timer.second += 1;

    if (this.timer.second == 60) {
      this.timer.second = 0;
      this.timer.minute += 1;
    }
    if (this.timer.minute == 60) {
      this.timer.minute = 0;
      this.timer.hour += 1;
    }
    let minute: any =
      this.timer.minute < 10 ? '0' + this.timer.minute : this.timer.minute;
    let second =
      this.timer.second < 10 ? '0' + this.timer.second : this.timer.second;
    let hour = this.timer.hour < 10 ? '0' + this.timer.hour : this.timer.hour;

    const total = hour + ':' + minute + ':' + second;

    this.horaView = total;
  }
  finaliza(tiempo: any) {
    this.final.second -= 1;

    if (this.final.second < 0) {
      this.final.second = 59;
      this.final.minute -= 1;
    }
    if (this.final.minute < 0) {
      this.final.minute = 59;
      this.final.hour -= 1;
    }
    if (this.final.hour < 0) {
      this.final.hour = 0;
      this.final.minute = 0;
      this.final.second = 0;
    }

    let minute: any =
      this.final.minute < 10 ? '0' + this.final.minute : this.final.minute;
    let second =
      this.final.second < 10 ? '0' + this.final.second : this.final.second;
    let hour = this.final.hour < 10 ? '0' + this.final.hour : this.final.hour;

    const total = hour + ':' + minute + ':' + second;

    this.horaFinal = total;
  }
  iniciar() {
    // -----Temporizador
    this.timerInterval = setInterval(() => {
      this.reloj(this.horaView);
      sessionStorage.setItem('fechaInicio', JSON.stringify(this.timer));
    }, 1000);

    // -----Temporizador
    this.finalInterval = setInterval(() => {
      this.finaliza(this.horaView);
      sessionStorage.setItem('fechaFinal', JSON.stringify(this.final));
    }, 1000);
    this.btnFinalizar = true;
    this.btnIniciar = false;
  }
  init() {
    this.iniciar();
    this.apiService
      .registrarAsistencia({
        id_Usuario: this.usuario.id_usuario,
        fecha_Inicio: new Date(),
      })
      .subscribe({ next: () => {}, error: () => {} });
  }
  finalizar() {
    this.btnFinalizar = false;
    this.btnIniciar = true;
    clearInterval(this.timerInterval);
    clearInterval(this.finalInterval);
    sessionStorage.removeItem('fechaInicio');
    sessionStorage.removeItem('fechaFinal');
    this.horaView = '00:00:00';
    this.horaFinal = '00:00:00';
    this.timer = {
      minute: 0,
      second: 0,
      hour: 0,
    };
    this.final = {
      minute: 0,
      second: 0,
      hour: 4,
    };
    this.apiService
      .registrarFin({
        id_Usuario: this.usuario.id_usuario,
        fecha_Fin: new Date(),
      })
      .subscribe({ next: () => {}, error: () => {} });
  }
}
