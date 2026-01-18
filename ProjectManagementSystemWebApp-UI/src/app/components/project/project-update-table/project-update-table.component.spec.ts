import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectUpdateTableComponent } from './project-update-table.component';

describe('ProjectUpdateTableComponent', () => {
  let component: ProjectUpdateTableComponent;
  let fixture: ComponentFixture<ProjectUpdateTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectUpdateTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProjectUpdateTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
