import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-action-button',
  standalone: false,
  templateUrl: './action-button.component.html',
  styleUrl: './action-button.component.css',
})
export class ActionButtonComponent<T> implements ICellRendererAngularComp {
  params!: ICellRendererParams;

  @Input() onRowClick!: (rowData: T) => void;
  @Output() updateClick = new EventEmitter<T>();
  @Output() deleteClick = new EventEmitter<T>();

  agInit(params: ICellRendererParams): void {
    this.params = params;
  }

  refresh(params: ICellRendererParams): boolean {
    return false;
  }

  onUpdateClick(): void {
    if (this.params?.context?.onUpdateClick) {
      this.params.context.onUpdateClick(this.params.data);
    }
    console.log(this.params.data);
    this.updateClick.emit(this.params.data);
  }

  onDeleteClick(): void {
    if (this.params?.context?.onDeleteClick) {
      this.params.context.onDeleteClick(this.params.data);
    }
    this.deleteClick.emit(this.params.data);
  }
}
