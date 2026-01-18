import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { IColumn } from '../../../models/column';
import { IFailedImport } from '../../../models/failed-Import';

@Component({
  selector: 'app-detailed-log',
  standalone: false,
  templateUrl: './detailed-log.component.html',
  styleUrl: './detailed-log.component.css',
})
export class DetailedLogComponent implements OnInit {
  columns: IColumn[] = [
    { field: 'id', headerName: 'ID', type: 'text' },
    { field: 'importId', headerName: 'ImportId', type: 'text' },
    { field: 'taskName', headerName: 'Task Name', type: 'text' },
    { field: 'failureReason', headerName: 'Failure Reason', type: 'longText' },
  ];
  failedImports!: IFailedImport[];

  constructor(
    private readonly _config: DynamicDialogConfig,
    private readonly _ref: DynamicDialogRef
  ) {}

  ngOnInit(): void {
    this.getFailedImports();
  }

  private getFailedImports(): void {
    if (this._config.data.log.failedImports) {
      this.failedImports = this._config.data.log.failedImports;
    }
  }
  
  closeDialog(): void {
    this._ref.close(null);
  }
}
