import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import type { ColDef, ValueGetterParams } from 'ag-grid-community';
import { AgTheme } from '../../core/themes/agthemes';
import { getStatusText } from '../../core/utils/task-status.utils';
import { IColumn } from '../../models/column';
import { ITask } from '../../models/task';
import { ActionButtonComponent } from '../cellRenderers/action-button/action-button.component';
import { AssignButtonComponent } from '../cellRenderers/assign-button/assign-button.component';
import { DateRendererComponent } from '../cellRenderers/date-renderer/date-renderer.component';
import { StatusRendererComponent } from '../cellRenderers/status-renderer/status-renderer.component';
import { ViewDetailComponent } from '../cellRenderers/view-detail/view-detail.component';

@Component({
  selector: 'app-table-grid',
  standalone: false,
  templateUrl: './table-grid.component.html',
  styleUrls: ['./table-grid.component.css'],
})
export class TableGridComponent<T> implements OnChanges {
  @Input() tableData: T[] = [];
  @Input() columns: IColumn[] = [];
  @Input() onRowClick!: (rowData: T) => void;
  @Input() onUpdateClick!: (rowData: T) => void;
  @Input() onDeleteClick!: (rowData: T) => void;
  @Input() onViewClick!: (task: ITask) => void;
  @Input() onAssignClick!: (rowData: T) => void;

  colDefs: ColDef[] = [];
  theme = AgTheme;
  pagination: boolean = true;
  paginationPageSize = 20;

  defaultColDef = {
    flex: 1,
    filter: true
  };

  constructor() {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tableData']) {
      this.handleTableDataChange(changes['tableData'].currentValue);
    }
    if (changes['columns']) {
      this.handleColumnsChange(changes['columns'].currentValue);
    }
  }

  private handleTableDataChange(tableData: T[]): void {
    this.tableData = tableData.map((row: T) => ({
      ...row,
      actions: '',
      ...this.handleNullValues(row)
    }));
  }

  private handleNullValues(row: T): T {
    return Object.fromEntries(
      Object.entries(row as unknown as Record<string, unknown>).map(
        ([key, value]) => [key, value ?? 'NA']
      )
    ) as T;
  }

  private handleColumnsChange(columns: IColumn[]): void {
    this.colDefs = columns.map((col: IColumn) => ({
      ...col,
      cellRenderer: this.getCellRenderer(col),
      cellRendererParams: this.getCellRendererParams(col),
      valueGetter: this.getValueGetter(col)
    }));
  }

  private getCellRenderer(col: IColumn) {
    if (col.field === 'actions') return ActionButtonComponent;
    if (col.type === 'date') return DateRendererComponent;
    if (col.type === 'status') return StatusRendererComponent;
    if (col.type === 'detail') return ViewDetailComponent;
    if (col.type === 'assign') return AssignButtonComponent;
    return undefined;
  }

  private getCellRendererParams(col: IColumn) {
    if (col.field === 'actions') {
      return {
        context: {
          onUpdateClick: this.onUpdateClick,
          onDeleteClick: this.onDeleteClick,
        },
      };
    } else if (col.type === 'detail') {
      return {
        context: {
          onViewClick: this.onViewClick,
        },
      };
    } else if (col.type === 'assign') {
      return {
        context: {
          onAssignClick: this.onAssignClick,
        },
      };
    }
    return undefined;
  }

  handleRowDoubleClick(event: any): void {
  if (this.onRowClick) {
    this.onRowClick(event.data);
  }
}


  private getValueGetter(col: IColumn) {
    if (col.type === 'status') {
      return (params: ValueGetterParams) =>
        this.parseStatus(
          params.data?.status ??
            params.data?.projectStatus ??
            params.data?.taskStatus ??
            null
        );
    }
    return undefined;
  }

  private parseStatus(status: number): string {
    return getStatusText(status);
  }
}
