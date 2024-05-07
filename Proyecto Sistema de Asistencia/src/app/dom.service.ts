import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable, Subject } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class DomService {
  private dataUsuario$ = new BehaviorSubject<any>(null);
  private dataLogin$ = new BehaviorSubject<any>(null);

  constructor() {}
  get obtDataUsuario$(): Observable<any> {
    return this.dataUsuario$.asObservable();
  }
  setDataUsuario(data: any): void {
    this.dataUsuario$.next(data);
  }
  get obtLogin$(): Observable<any> {
    return this.dataLogin$.asObservable();
  }
  setLogin(data: any): void {
    this.dataLogin$.next(data);
  }

  setItem(key: string, value: any) {
    if (typeof sessionStorage !== 'undefined') {
      sessionStorage.setItem(key, JSON.stringify(value));
    } else {
      console.error('sessionStorage is not available.');
    }
  }

  getItem(key: string): any {
    if (typeof sessionStorage !== 'undefined') {
      const item = sessionStorage.getItem(key);
      return item ? JSON.parse(item) : null;
    } else {
      console.error('sessionStorage is not available.');
      return null;
    }
  }

  removeItem(key: string) {
    if (typeof sessionStorage !== 'undefined') {
      sessionStorage.removeItem(key);
    } else {
      console.error('sessionStorage is not available.');
    }
  }
}
