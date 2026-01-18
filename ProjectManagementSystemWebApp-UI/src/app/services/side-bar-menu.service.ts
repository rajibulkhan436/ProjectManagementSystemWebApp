import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClientService } from '../core/http-client.service';
import { ICategory } from '../models/feature';

@Injectable({
  providedIn: 'root',
})
export class SideBarMenuService {
  private readonly _apiUrl: string = 'menu/DisplayFeaturesMenu';
  constructor(private readonly _httpClientService: HttpClientService) {}

  fetchFeatures(): Observable<ICategory[]> {
    return this._httpClientService.get<ICategory[]>(this._apiUrl);
  }
}
