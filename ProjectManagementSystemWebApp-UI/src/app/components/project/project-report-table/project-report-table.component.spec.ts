import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectReportTableComponent } from './project-report-table.component';

describe('ProjectReportTableComponent', () => {
  let component: ProjectReportTableComponent;
  let fixture: ComponentFixture<ProjectReportTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectReportTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProjectReportTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
