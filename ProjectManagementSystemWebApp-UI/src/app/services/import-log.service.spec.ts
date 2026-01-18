import { TestBed } from '@angular/core/testing';

import { ImportLogService } from './import-log.service';

describe('ImportLogService', () => {
  let service: ImportLogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ImportLogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
