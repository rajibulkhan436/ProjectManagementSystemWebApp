import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateRendererComponent } from './date-renderer.component';

describe('DateRendererComponent', () => {
  let component: DateRendererComponent;
  let fixture: ComponentFixture<DateRendererComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DateRendererComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
