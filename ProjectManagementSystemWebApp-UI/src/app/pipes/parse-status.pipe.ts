import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'parseStatus',
  standalone: true,
})
export class ParseStatusPipe implements PipeTransform {
  transform(value: number, ...args: unknown[]): string {
    switch (value) {
      case 1:
        return 'NotStarted';
      case 2:
        return 'InProgress';
      case 3:
        return 'Completed';
      default:
        return 'None';
    }
  }
}
