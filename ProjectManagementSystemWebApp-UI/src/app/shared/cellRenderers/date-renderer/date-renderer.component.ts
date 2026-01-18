import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-date-renderer',
  standalone: false,
  template: `{{ formattedDate }}`,
  styles: ``,
})
export class DateRendererComponent implements ICellRendererAngularComp {
  formattedDate: string = '';
  params!: ICellRendererParams;

  agInit(params: ICellRendererParams): void {
    this.params = params;
    this.formattedDate = this.formatDate(params.value);
  }

  refresh(params: ICellRendererParams): boolean {
    return false;
  }

  private formatDate(dateString: string): string {
    if (dateString === 'NA') {
      return dateString;
    }
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      month: 'long',
      day: 'numeric',
      year: 'numeric',
      hour: 'numeric',
      minute: '2-digit',
      hour12: true,
    });
  }
}
