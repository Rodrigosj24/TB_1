import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ApiService } from '../api.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ModalErrorComponent } from '../modal-error/modal-error.component';
import { MatDialog } from '@angular/material/dialog';
import { ModalExitosoComponent } from '../modal-exitoso/modal-exitoso.component';
export interface UserData {
  nombres: string;
  correo: string;
  telefono: string;
  rol: string;
  delete: string;
}

@Component({
  selector: 'app-listar-usuario',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './listar-usuario.component.html',
  styleUrl: './listar-usuario.component.css',
})
export class ListarUsuarioComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = [
    'nombres_apellidos',
    'correo',
    'celular',
    'rol',
    'descripcion_estado',
    'id_usuario',
  ];

  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private apiService: ApiService, public dialog: MatDialog) {
    // Create 100 users
    const users: any = [
      {
        nombres_apellidos: 'n',
        correo: 'n',
        telefono: 'n',
        rol: 'n',
        id_usuario: 'n',
      },
      {
        nombres_apellidos: 'a',
        correo: 'sn',
        telefono: 'fn',
        rol: 'rn',
        id_usuario: 'rn',
      },
    ];

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
  }
  ngOnInit(): void {
    this.apiService.getUsuarios().subscribe({
      next: (resp) => {
        // this.dataSource = new MatTableDataSource(resp.data);

        const modifiedData = resp.data.map((item: any) => {
          return {
            nombres_apellidos: item.nombres_apellidos,
            correo: item.correo,
            celular: item.celular,
            rol: item.rol,
            descripcion_estado: item.descripcion_estado,
            id_usuario: item.correo,
            // Agrega más campos o modifica los existentes según tus necesidades
          };
        });
        this.dataSource = new MatTableDataSource(modifiedData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
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

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  eliminarUsuario(correo: any) {
    this.apiService.deleteUser(correo).subscribe({
      next: () => {
        this.apiService.getUsuarios().subscribe({
          next: (resp) => {
            // this.dataSource = new MatTableDataSource(resp.data);
            this.mostrarMensajeExito();
            const modifiedData = resp.data.map((item: any) => {
              return {
                nombres_apellidos: item.nombres_apellidos,
                correo: item.correo,
                celular: item.celular,
                rol: item.rol,
                descripcion_estado: item.descripcion_estado,
                id_usuario: item.correo,
                // Agrega más campos o modifica los existentes según tus necesidades
              };
            });
            this.dataSource = new MatTableDataSource(modifiedData);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
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
      },
      error: (err) => {
        if (err.error.message) {
          this.openErrorDialog('0ms', '0ms', err.error.message);
        }
      },
    });
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
      // Puedes realizar acciones adicionales después de cerrar el modal
    });
  }

}
