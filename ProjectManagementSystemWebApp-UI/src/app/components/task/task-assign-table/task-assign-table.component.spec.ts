import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskAssignTableComponent } from './task-assign-table.component';

describe('TaskAssignTableComponent', () => {
  let component: TaskAssignTableComponent;
  let fixture: ComponentFixture<TaskAssignTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TaskAssignTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskAssignTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
