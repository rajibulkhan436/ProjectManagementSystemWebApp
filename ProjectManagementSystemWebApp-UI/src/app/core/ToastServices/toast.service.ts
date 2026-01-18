import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Severity } from '../../models/severity';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor(private readonly _messageService: MessageService) {}

  showToast(
    messageSeverity: Severity,
    summaryMessage: string,
    message: string
  ) {
    this._messageService.add({
      severity: messageSeverity,
      summary: summaryMessage,
      detail: message,
    });
  }
}
