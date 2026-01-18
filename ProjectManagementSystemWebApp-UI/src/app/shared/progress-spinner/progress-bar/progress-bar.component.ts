import { Component } from '@angular/core';
import { ProgressSpinnerHubService } from '../shared/progress-spinner-hub.service';

@Component({
  selector: 'app-progress-bar',
  standalone: false,
  templateUrl: './progress-bar.component.html',
  styleUrl: './progress-bar.component.css',
})
export class ProgressBarComponent {
  progressPercentage: number = 0;
  isVisible: boolean = false;

  constructor(private readonly _progressService: ProgressSpinnerHubService) {}

  ngOnInit(): void {
    this._progressService.progress.subscribe(
      (progress) => (this.progressPercentage = progress)
    );
    this._progressService.isVisible.subscribe(
      (visible) => (this.isVisible = visible)
    );
  }
}
