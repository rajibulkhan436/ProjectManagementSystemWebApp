import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { IImportLog } from '../../../models/import-log';
import { Severity } from '../../../models/severity';
import { ImportLogService } from '../../../services/import-log.service';
import { DetailedLogComponent } from '../detailed-log/detailed-log.component';

@Component({
  selector: 'app-import-log',
  standalone: false,
  templateUrl: './import-log.component.html',
  styleUrl: './import-log.component.css',
  providers: [DialogService],
})
export class ImportLogComponent implements OnInit {
  logs: IImportLog[] = [];
  columns: IColumn[] = [
    { field: 'id', headerName: 'ID', type: 'text' },
    { field: 'fileName', headerName: 'File Name', type: 'text' },
    { field: 'uploadTime', headerName: 'Upload Time', type: 'date' },
    { field: 'total', headerName: 'Total Tasks', type: 'text' },
    { field: 'success', headerName: 'Successful Imports', type: 'text' },
    { field: 'fails', headerName: 'Failed Imports', type: 'text' },
  ];

  constructor(
    private readonly _dialogService: DialogService,
    private readonly _logService: ImportLogService,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.getImportLogs();
  }

  private getImportLogs(): void {
    this._logService.fetchImportLog().subscribe({
      next: (logs: IImportLog[]) => {
        this.logs = logs;
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error fetching ImportLogs',
          error.message
        );
      },
    });
  }

  openFailedImportDialog(log: IImportLog): void {
    this._dialogService.open(DetailedLogComponent, {
      header: 'Failed Imports',
      width: '60%',
      height: '70%',
      modal: true,
      data: { log },
    });
  }
}
