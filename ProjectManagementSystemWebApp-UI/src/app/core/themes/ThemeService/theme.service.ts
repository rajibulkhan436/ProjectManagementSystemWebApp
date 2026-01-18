import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private readonly _isDarkModeSubject = new BehaviorSubject<boolean>(
    this.getStoredTheme()
  );
  isDarkMode = this._isDarkModeSubject.asObservable();

  constructor() {
    this.applyTheme(this._isDarkModeSubject.value);
  }

  toggleDarkMode(): void {
    const newMode = !this._isDarkModeSubject.value;
    this._isDarkModeSubject.next(newMode);
    this.applyTheme(newMode);
    localStorage.setItem('darkMode', JSON.stringify(newMode));
  }

  private applyTheme(isDark: boolean): void {
    const htmlElement = document.documentElement;
    if (isDark) {
      htmlElement.classList.add('dark-mode');
    } else {
      htmlElement.classList.remove('dark-mode');
    }
  }

  private getStoredTheme(): boolean {
    return localStorage.getItem('darkMode') === 'true';
  }
}
