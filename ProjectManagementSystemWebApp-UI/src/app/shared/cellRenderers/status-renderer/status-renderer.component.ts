import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-status-renderer',
  standalone: false,
  template: `
    <span [ngStyle]="{ color: getStatusColor(params.value) }">
      {{ params.value }}
    </span>
  `,
  styles: ``,
})
export class StatusRendererComponent implements ICellRendererAngularComp {
  params!: ICellRendererParams;

  agInit(params: ICellRendererParams): void {
    this.params = params;
  }
  refresh(params: ICellRendererParams): boolean {
    return false;
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'NotStarted':
        return 'red';
      case 'InProgress':
        return 'orange';
      case 'Completed':
        return 'green';
      default:
        return 'black';
    }
  }
}
