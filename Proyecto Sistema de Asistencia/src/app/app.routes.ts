import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NavComponent } from './nav/nav.component';
import { MarcarAsistenciaComponent } from './marcar-asistencia/marcar-asistencia.component';
import { UsuarioComponent } from './usuario/usuario.component';
import { ListarUsuarioComponent } from './listar-usuario/listar-usuario.component';

export const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'home',
    component: NavComponent,

    children: [
      { path: '', redirectTo: 'marcar-asistencia', pathMatch: 'full' },
      { path: 'marcar-asistencia', component: MarcarAsistenciaComponent },
      { path: 'usuario', component: UsuarioComponent },
      { path: 'listar-usuario', component: ListarUsuarioComponent },
    ],
  },
];
