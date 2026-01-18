import { TestBed } from '@angular/core/testing';

import { ProgressSpinnerHubService } from './progress-spinner-hub.service';

describe('ProgressSpinnerHubService', () => {
  let service: ProgressSpinnerHubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProgressSpinnerHubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
