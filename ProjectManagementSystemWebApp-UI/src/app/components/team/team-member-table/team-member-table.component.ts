import { Component } from '@angular/core';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { Severity } from '../../../models/severity';
import { ITeamMember } from '../../../models/team-member';
import { TeamService } from '../../../services/team.service';

@Component({
  selector: 'app-team-member-table',
  standalone: false,
  templateUrl: './team-member-table.component.html',
  styleUrl: './team-member-table.component.css',
})
export class TeamMemberTableComponent {
  teams: ITeamMember[] = [];
  columns: IColumn[] = [
    { field: 'id', headerName: 'Id', type: 'text' },
    { field: 'name', headerName: 'Name', type: 'text' },
    { field: 'role', headerName: 'Role', type: 'text' }
  ];

  constructor(
    private readonly _teamService: TeamService,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.getTeamMembers();
  }

  private getTeamMembers(): void {
    this._teamService.fetchTeamMembers().subscribe({
      next: (teamMember: ITeamMember[]) => {
        this.teams = teamMember;
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
