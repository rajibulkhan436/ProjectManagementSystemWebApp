import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClientService } from '../../../core/http-client.service';
import { ILogin } from '../../../models/login';
import { IUser } from '../../../models/user';

@Injectable({ providedIn: 'root' })
export class AuthHubService {
  private readonly _apiUrl = 'authentication';

  constructor(private readonly _http: HttpClientService) {}

  login(credentials: ILogin): Observable<ILogin> {
    return this._http.post<ILogin>(`${this._apiUrl}/login`, credentials);
  }

  register(user: IUser): Observable<IUser> {
    return this._http.post<IUser>(`${this._apiUrl}/register`, user);
  }
}
