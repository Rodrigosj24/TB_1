import { Component, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-modal-exitoso',
  standalone: true,
  imports: [MatIconModule, MatDialogModule, MatButtonModule],
  templateUrl: './modal-exitoso.component.html',
  styleUrl: './modal-exitoso.component.css'
})
export class ModalExitosoComponent {
  constructor(private dialog: MatDialog,
    public dialogRef: MatDialogRef<ModalExitosoComponent>,  
    @Inject(MAT_DIALOG_DATA) public data: any = []) {
      dialogRef.disableClose = true;
    }

  mostrarMensajeExito() {
    const dialogRef = this.dialog.open(ModalExitosoComponent, {
      width: '400px', // Ancho del modal
    });

    // Opcional: Manejar acciones después de cerrar el modal
    dialogRef.afterClosed().subscribe(() => {
      console.log('Modal cerrado');
      // Puedes realizar acciones adicionales después de cerrar el modal
    });
  }

  close() {
    this.dialogRef.close(true);
  }

}
