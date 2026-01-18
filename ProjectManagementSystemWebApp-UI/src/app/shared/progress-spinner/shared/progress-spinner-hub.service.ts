import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProgressSpinnerHubService {
  constructor() {}
  loader = new BehaviorSubject<boolean>(false);
  private readonly _progress = new BehaviorSubject<number>(0);
  private readonly _isVisible = new BehaviorSubject<boolean>(false);
  progress = this._progress.asObservable();
  isVisible = this._isVisible.asObservable();

  showProgressBar(): void {
    this._isVisible.next(true);
    this.simulateProgress();
  }

  setProgress(value: number): void {
    this._progress.next(Math.min(value, 100));
    if (value >= 100) {
      setTimeout(() => this.hideProgressBar(), 500);
    }
  }

  hideProgressBar(): void {
    this._progress.next(0);
    this._isVisible.next(false);
  }

  private simulateProgress(): void {
    let current = 0;
    const interval = setInterval(() => {
      current += Math.floor(Math.random() * 10) + 5;
      this._progress.next(Math.min(current, 95));
      if (current >= 95) clearInterval(interval);
    }, 500);
  }
}
