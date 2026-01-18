import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProgressSpinnerHubService } from './shared/progress-spinner-hub.service';

@Component({
  selector: 'app-progress-spinner',
  standalone: false,
  templateUrl: './progress-spinner.component.html',
  styleUrl: './progress-spinner.component.css',
})
export class ProgressSpinnerComponent implements OnDestroy {
  isLoading = true;
  private readonly _subscription!: Subscription;
  constructor(private readonly _spinnerService: ProgressSpinnerHubService) {
    this._subscription = this._spinnerService.loader.subscribe((res) => {
      this.isLoading = res;
    });
  }

  ngOnDestroy(): void {
    if (this._subscription) {
      this._subscription.unsubscribe();
    }
  }
}
