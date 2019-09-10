import { TestBed } from '@angular/core/testing';

import { DreamlineService } from './dreamline.service';

describe('DreamlineService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DreamlineService = TestBed.get(DreamlineService);
    expect(service).toBeTruthy();
  });
});
