import { Pipe, PipeTransform } from '@angular/core';
import { getStatusClass } from '../core/utils/task-status.utils';

@Pipe({ name: 'statusClass',standalone:false })
export class StatusClassPipe implements PipeTransform {
  transform(status: number): string {
    return getStatusClass(status);
  }
}

