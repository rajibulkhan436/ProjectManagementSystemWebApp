import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Routes } from '../core/constants/routes';
import { HttpClientService } from '../core/http-client.service';
import { IImportLog } from '../models/import-log';

@Injectable({
  providedIn: 'root',
})
export class ImportLogService {
  constructor(private readonly _httpClientService: HttpClientService) {}

  fetchImportLog(): Observable<IImportLog[]> {
    return this._httpClientService.get<IImportLog[]>(Routes.displayImportLog);
  }
}
