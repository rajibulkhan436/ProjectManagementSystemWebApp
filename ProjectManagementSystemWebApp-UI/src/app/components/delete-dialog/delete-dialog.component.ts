import { Component } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-delete-dialog',
  standalone: false,
  templateUrl: './delete-dialog.component.html',
  styleUrl: './delete-dialog.component.css',
})
export class DeleteDialogComponent {
  message: string = '';
  trashIcon: string = 'pi pi-exclamation-triangle';

  constructor(
    private readonly _ref: DynamicDialogRef,
    private readonly _config: DynamicDialogConfig
  ) {
    this.initializeDialog();
  }

  initializeDialog(): void {
    if (this._config.data) {
      this.message =
        this._config.data.message || 'Are you sure you want to proceed?';
        this.trashIcon = this._config.data.icon || 'pi pi-exclamation-triangle';
    }
  }

  confirm(): void {
    this._ref.close(true);
  }

  cancel(): void {
    this._ref.close(false);
  }
}
