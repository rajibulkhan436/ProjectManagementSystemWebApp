import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SessionHelperService {
  constructor() {}

  set<T>(key: string, value: T): void {
    sessionStorage.setItem(key, JSON.stringify(value));
  }

  get(key: string): string | null {
    const checkKey:boolean = sessionStorage.hasOwnProperty(key);
    if (checkKey) {
      return JSON.parse(sessionStorage.getItem(key)!);
    }
    return null;
  }

  isLoggedIn(): boolean {
    return !!sessionStorage.getItem('token');
  }

  remove(key: string): void {
    const checkKey = sessionStorage.hasOwnProperty(key);
    if (checkKey) {
      sessionStorage.removeItem(key);
    }
  }

  deleteSession(): void {
    sessionStorage.clear();
  }
}
