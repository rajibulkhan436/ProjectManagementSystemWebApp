import { Component } from '@angular/core';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { IProjectReport } from '../../../models/project-report';
import { Severity } from '../../../models/severity';
import { ProjectService } from '../../../services/project.service';

@Component({
  selector: 'app-project-report-table',
  standalone: false,
  templateUrl: './project-report-table.component.html',
  styleUrl: '../project-table/project-table.component.css',
})
export class ProjectReportTableComponent {
  title: string = 'projectReport';
  projectReports: IProjectReport[] = [];
  columns: IColumn[] = [
    { field: 'projectName', headerName: 'Project Name', type: 'text' },
    { field: 'taskName', headerName: 'Task Name', type: 'text' },
    { field: 'taskStatus', headerName: 'Task Status', type: 'status' },
    { field: 'dueDate', headerName: 'due Date', type: 'date' },
    { field: 'projectStatus', headerName: 'Project Status', type: 'status' }
  ];

  constructor(
    private readonly _projectService: ProjectService,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.getProjectReports();
  }

  private getProjectReports(): void {
    this._projectService.fetchProjectReports().subscribe({
      next: (projectReports: IProjectReport[]) => {
        this.projectReports = projectReports;
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error fetching projects:',
          error.message
        );
      }
    });
  }
}
