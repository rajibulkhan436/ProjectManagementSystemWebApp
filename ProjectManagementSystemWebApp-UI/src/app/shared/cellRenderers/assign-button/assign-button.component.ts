import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-assign-button',
  standalone: false,
  templateUrl: './assign-button.component.html',
  styleUrl: './assign-button.component.css',
})
export class AssignButtonComponent implements ICellRendererAngularComp {
  params!: ICellRendererParams;

  agInit(params: ICellRendererParams): void {
    this.params = params;
  }
  refresh(params: ICellRendererParams): boolean {
    return false;
  }

  onAssign(): void {
     if (this.params?.data) {
       this.params.context.onAssignClick(this.params.data);
     }
  }
}
