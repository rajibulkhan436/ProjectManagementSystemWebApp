import { Component, EventEmitter, Output } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { IProject } from '../../../models/project';
import { Severity } from '../../../models/severity';
import { ProjectService } from '../../../services/project.service';
import { DeleteDialogComponent } from '../../delete-dialog/delete-dialog.component';
import { UpdateDialogComponent } from '../../update-dialog/update-dialog.component';

@Component({
  selector: 'app-project-update-table',
  standalone: false,
  templateUrl: './project-update-table.component.html',
  styleUrls: ['./project-update-table.component.css'],
  providers: [DialogService],
})
export class ProjectUpdateTableComponent {
  @Output() emitClick = new EventEmitter<IProject>();
  title: string = 'projects';
  projects: IProject[] = [];
  columns: IColumn[] = [
    { field: 'projectId', headerName: 'ID', type: 'text' },
    { field: 'projectName', headerName: 'Project Name', type: 'text' },
    { field: 'startDate', headerName: 'Start Date', type: 'date' },
    { field: 'endDate', headerName: 'End Date', type: 'date' },
    { field: 'status', headerName: 'Status', type: 'status' },
    {
      field: 'actions',
      headerName: 'Action',
      type: 'action',
    }
  ];

  constructor(
    private readonly _projectService: ProjectService,
    private readonly _dialogService: DialogService,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.getProjects();
  }

  private getProjects(): void {
    this._projectService.fetchProjects().subscribe({
      next: (projects: IProject[]) => {
        this.projects = projects;
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

  onRowClick(project: IProject): void {
    this.openUpdateDialog(project);
  }

  openUpdateDialog(project: IProject): void {
    const ref = this._dialogService.open(UpdateDialogComponent, {
      header: 'Update Project',
      width: '40%',
      height: '70%',
      data: {
        entity: project,
        columns: this.columns.filter((col) => col.field !== 'actions'),
      },
      contentStyle: { overflow: 'auto' },
      modal: true
    });

    ref.onClose.subscribe((updatedProject: IProject) => {
      if (
        updatedProject &&
        updatedProject === undefined &&
        updatedProject === null
      ) {
        this.updateProjectStatus(updatedProject);
      }
    });
  }

  updateProjectStatus(updatedProject: IProject): void {
    this._projectService.updateProject(updatedProject).subscribe({
      next: () => {
        this._toastService.showToast(
          Severity.success,
          'Project updated successfully!',
          'Your changes have been saved.'
        );
        this.getProjects();
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error updating project:',
          error.message
        );
      }
    });
  }

  openDeleteDialog(project: IProject): void {
    const ref = this._dialogService.open(DeleteDialogComponent, {
      header: 'Confirm Deletion',
      width: '30%',
      data: {
        message: `Are you sure you want to delete "${project.projectName}"?`,
        icon: 'pi pi-trash',
      },
      modal: true
    });

    ref.onClose.subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.deleteRecord(project);
      }
    });
  }

  deleteRecord(project: IProject): void {
    this._projectService.deleteProject(project.projectId).subscribe({
      next: () => {
        this._toastService.showToast(
          Severity.success,
          'Project deleted successfully!',
          `Project "${project.projectName}" has been removed.`
        );
        this.getProjects();
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error deleting project:',
          error.message
        );
      }
    });
  }
}
