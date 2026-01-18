import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailedLogComponent } from './detailed-log.component';

describe('DetailedLogComponent', () => {
  let component: DetailedLogComponent;
  let fixture: ComponentFixture<DetailedLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DetailedLogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailedLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
