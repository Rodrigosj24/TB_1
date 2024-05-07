import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
@Component({
  selector: 'app-modal-error',
  standalone: true,
  imports: [MatIconModule, MatDialogModule, MatButtonModule],
  templateUrl: './modal-error.component.html',
  styleUrl: './modal-error.component.css',
})
export class ModalErrorComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ModalErrorComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any = []
  ) {}
  ngOnInit(): void {}

  procesarTexto(texto: string): string {
    return texto.replace(/\./g, '<br>');
  }

  close() {
    this.dialogRef.close(true);
  }
}
