import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesunitsComponent } from './salesunits.component';

describe('SalesunitsComponent', () => {
  let component: SalesunitsComponent;
  let fixture: ComponentFixture<SalesunitsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalesunitsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesunitsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
