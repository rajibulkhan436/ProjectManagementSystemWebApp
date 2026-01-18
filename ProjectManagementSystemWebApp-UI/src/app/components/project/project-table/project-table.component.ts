import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { IProject } from '../../../models/project';
import { Severity } from '../../../models/severity';
import { ProjectService } from '../../../services/project.service';
import { NotificationService } from '../../../services/signal-r.service';

@Component({
  selector: 'app-project-table',
  standalone: false,
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.css'],
})
export class ProjectTableComponent implements OnInit {
  title: string = 'projects';
  projects: IProject[] = [];
  columns: IColumn[] = [
    { field: 'projectId', headerName: 'ID', type: 'text' },
    { field: 'projectName', headerName: 'Project Name', type: 'text' },
    { field: 'startDate', headerName: 'Start Date', type: 'date' },
    { field: 'endDate', headerName: 'End Date', type: 'date' },
    { field: 'status', headerName: 'Status', type: 'status' }
  ];

  notifications: string[] = [];
  constructor(
    private readonly _projectService: ProjectService,
    private readonly _toastService: ToastService,
    private readonly _notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.sendMessage();
    this.getProjects();
    this.sendMessage();
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
      },
    });
  }

  sendMessage(): void {
    this._notificationService.broadcastMessage((message: string):void=>{
      this.notifications.unshift(message)
      this._toastService.showToast(
        Severity.info,
        "Notification Received",
        message
      )
    });
  }
}
