import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { Environment } from '../../environments/environment';
import { ProgressSpinnerHubService } from '../shared/progress-spinner/shared/progress-spinner-hub.service';

@Injectable({
  providedIn: 'root',
})
export class HttpClientService {
  constructor(
    private readonly _http: HttpClient,
    private readonly _spinnerService: ProgressSpinnerHubService
  ) {}

  get<T>(url: string, params?: Params): Observable<T> {
    this._spinnerService.loader.next(true);
    return this._http
      .get<T>(`${Environment.baseUrl}/${url}`, {
        headers: this.getHeader(),
        params,
      })
      .pipe(
        tap(() => this._spinnerService.loader.next(true)),
        finalize(() => this._spinnerService.loader.next(false)),
        catchError(this.handleError)
      );
  }

  post<T>(url: string, body: T): Observable<T> {
    this._spinnerService.loader.next(true);
    return this._http
      .post<T>(`${Environment.baseUrl}/${url}`, body, {
        headers: this.getHeader(),
      })
      .pipe(
        tap(() => this._spinnerService.loader.next(true)),
        finalize(() => this._spinnerService.loader.next(false)),
        catchError(this.handleError)
      );
  }

  put<T>(url: string, body: T): Observable<T> {
    this._spinnerService.loader.next(true);
    return this._http
      .put<T>(`${Environment.baseUrl}/${url}`, body, {
        headers: this.getHeader(),
      })
      .pipe(
        tap(() => this._spinnerService.loader.next(true)),
        finalize(() => this._spinnerService.loader.next(false)),
        catchError(this.handleError)
      );
  }

  delete<T>(
    url: string,
    params?: { [key: string]: any },
    body?: T
  ): Observable<T> {
    this._spinnerService.loader.next(true);

    let httpParams = new HttpParams();
    if (params) {
      Object.keys(params).forEach((key) => {
        httpParams = httpParams.append(key, params[key]);
      });
    }

    return this._http
      .delete<T>(`${Environment.baseUrl}/${url}`, {
        headers: this.getHeader(),
        params: httpParams,
        body: body,
      })
      .pipe(
        finalize(() => this._spinnerService.loader.next(false)),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    return throwError(
      (): Error => new Error(error.message || 'Something went wrong')
    );
  }

  private getHeader(): HttpHeaders {
    let token: string | null = sessionStorage.getItem('token');

    if (token) {
      token = token.replace(/^"(.*)"$/, '$1');
    }
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    return headers;
  }
}
