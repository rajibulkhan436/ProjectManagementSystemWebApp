import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskUpdateTableComponent } from './task-update-table.component';

describe('TaskUpdateTableComponent', () => {
  let component: TaskUpdateTableComponent;
  let fixture: ComponentFixture<TaskUpdateTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TaskUpdateTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskUpdateTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
