import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { Severity } from '../../../models/severity';
import { ITaskTeam } from '../../../models/task-team';
import { TeamService } from '../../../services/team.service';

@Component({
  selector: 'app-task-team-table',
  standalone: false,
  templateUrl: './task-team-table.component.html',
  styleUrl: './task-team-table.component.css',
})
export class TaskTeamTableComponent implements OnInit {
  title: string = 'TaskTeam';
  taskTeams: ITaskTeam[] = [];
  columns: IColumn[] = [
    { field: 'taskId', headerName: 'Task Id', type: 'text' },
    { field: 'taskName', headerName: 'Task Name', type: 'text' },
    { field: 'teamMemberId', headerName: 'Member Id', type: 'text' },
    { field: 'teamMemberName', headerName: 'Member Name', type: 'text' },
    { field: 'assignedDate', headerName: 'Assigned Date', type: 'date' }
  ];

  constructor(
    private readonly _teamService: TeamService,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.getTaskTeams();
  }

  private getTaskTeams(): void {
    this._teamService.fetchTaskTeams().subscribe({
      next: (taskTeams: ITaskTeam[]) => {
        this.taskTeams = taskTeams;
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error fetching Task Teams:',
          error.message
        );
      }
    });
  }
}
