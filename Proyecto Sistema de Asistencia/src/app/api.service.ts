import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  baseURL = environment.baseURL;
  headers = {
    'Content-Type': 'application/json',
  };
  private _refresh$ = new Subject<void>();
  constructor(private http: HttpClient) {}
  get refresh$() {
    return this._refresh$;
  }
  getTipificacion(getDatTip: any): Observable<any> {
    const path =
      '/Administracion/Obtener_Tipificacion?cod_servicio=' +
      getDatTip.cod_servicio +
      '&queue=' +
      getDatTip.queue;

    return this.http.get<any>(this.baseURL + path);
  }
  postCrearGestion(crearGestionData: any): Observable<any> {
    const body = JSON.stringify(crearGestionData);
    return this.http
      .post<any>(
        this.baseURL + '/Administracion/Crear_Gestion_UnivPacifico',
        body,
        {
          headers: this.headers,
        }
      )
      .pipe(
        tap(() => {
          this._refresh$.next();
        })
      );
  }
  loginuser(data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http
      .post<any>(this.baseURL + '/Usuario/Obtener_Login_Usuario', body, {
        headers: this.headers,
      })
      .pipe(
        tap(() => {
          this._refresh$.next();
        })
      );
  }
  registrarUsuario(data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http
      .post<any>(this.baseURL + '/Usuario/Registrar_Usuario', body, {
        headers: this.headers,
      })
      .pipe(
        tap(() => {
          this._refresh$.next();
        })
      );
  }
  getRoles(): Observable<any> {
    const path = '/Usuario/Obtener_Roles';

    return this.http.get<any>(this.baseURL + path);
  }
  getUsuarios(): Observable<any> {
    const path = '/Usuario/Listar_Datos_Usuarios';

    return this.http.get<any>(this.baseURL + path);
  }
  registrarAsistencia(data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http
      .post<any>(
        this.baseURL + '/Asistencia/Registrar_Inicio_Asistencia',
        body,
        {
          headers: this.headers,
        }
      )
      .pipe(
        tap(() => {
          this._refresh$.next();
        })
      );
  }
  registrarFin(data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http
      .post<any>(this.baseURL + '/Asistencia/Registrar_Fin_Asistencia', body, {
        headers: this.headers,
      })
      .pipe(
        tap(() => {
          this._refresh$.next();
        })
      );
  }
  deleteUser(data: any): Observable<any> {
    const path = '/Usuario/Eliminar_Usuario?Correo=' + data;

    return this.http.get<any>(this.baseURL + path);
  }
}
