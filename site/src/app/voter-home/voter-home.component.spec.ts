import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VoterHomeComponent } from './voter-home.component';

describe('VoterHomeComponent', () => {
  let component: VoterHomeComponent;
  let fixture: ComponentFixture<VoterHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VoterHomeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VoterHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
