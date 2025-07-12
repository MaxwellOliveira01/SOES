import { TestBed } from '@angular/core/testing';

import { KeyGenerationService } from './key-generation.service';

describe('KeyGenerationService', () => {
  let service: KeyGenerationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KeyGenerationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
