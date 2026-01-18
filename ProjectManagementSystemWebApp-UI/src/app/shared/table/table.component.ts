import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IAction } from '../../models/actions';
import { IColumn } from '../../models/column';

@Component({
  selector: 'app-table',
  standalone: false,
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent<T> {
  @Input() tableData: T[] = [];
  @Input() title: string = '';
  @Input() columns: IColumn[] = [];
  @Input() actions: IAction<T>[] = [];
  @Input() onRowClick!: (rowData: T) => void;
  @Output() emitClick = new EventEmitter<T>();

  constructor() {
  }

  getSeverity(status: number) {
    switch (status) {
      case 1:
        return 'danger';
      case 2:
        return 'warn';
      case 3:
        return 'success';
      default:
        return 'info';
    }
  }

  onClick(rowData: T): void {
   if (this.onRowClick) {
      this.onRowClick(rowData);
    }
    this.emitClick.emit(rowData);
  }
}
