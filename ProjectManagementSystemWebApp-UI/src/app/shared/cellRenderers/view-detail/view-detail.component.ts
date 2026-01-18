import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-view-detail',
  standalone: false,
  templateUrl: './view-detail.component.html',
  styleUrl: './view-detail.component.css',
})
export class ViewDetailComponent implements ICellRendererAngularComp {
  params!: ICellRendererParams;

  agInit(params: ICellRendererParams): void {
    this.params = params;
  }
  refresh(params: ICellRendererParams): boolean {
    return false;
  }

  viewDetail(): void {
    console.log(this.params?.data);
    this.params.context.onViewClick(this.params?.data);
  }
}
