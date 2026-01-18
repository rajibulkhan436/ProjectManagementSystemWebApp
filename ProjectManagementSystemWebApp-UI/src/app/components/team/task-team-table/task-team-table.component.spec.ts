import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskTeamTableComponent } from './task-team-table.component';

describe('TaskTeamTableComponent', () => {
  let component: TaskTeamTableComponent;
  let fixture: ComponentFixture<TaskTeamTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TaskTeamTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskTeamTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
