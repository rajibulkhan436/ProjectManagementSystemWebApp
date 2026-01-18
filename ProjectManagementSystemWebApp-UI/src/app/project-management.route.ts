import { Routes } from '@angular/router';
import { ProjectReportTableComponent } from './components/project/project-report-table/project-report-table.component';
import { ProjectTableComponent } from './components/project/project-table/project-table.component';
import { ProjectUpdateTableComponent } from './components/project/project-update-table/project-update-table.component';
import { ImportLogComponent } from './components/task/import-log/import-log.component';
import { TaskAssignTableComponent } from './components/task/task-assign-table/task-assign-table.component';
import { TaskTableComponent } from './components/task/task-table/task-table.component';
import { TaskUpdateTableComponent } from './components/task/task-update-table/task-update-table.component';
import { TaskTeamTableComponent } from './components/team/task-team-table/task-team-table.component';
import { TeamMemberTableComponent } from './components/team/team-member-table/team-member-table.component';
import { AuthGuard } from './core/guard/auth.guard';
import { ProjectManagementComponent } from './project-management.component';

export const ProjectManagementRoutes: Routes = [
  {
    path: '',
    component: ProjectManagementComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'project', pathMatch: 'full' },
      { path: 'project/status', component: ProjectTableComponent },
      { path: 'project/update-status', component: ProjectUpdateTableComponent },
      { path: 'project/reports', component: ProjectReportTableComponent },
      { path: 'task/status', component: TaskTableComponent },
      { path: 'task/update-status', component: TaskUpdateTableComponent },
      { path: 'task/assign-task-team', component: TaskAssignTableComponent },
      { path: 'team/team-members', component: TeamMemberTableComponent },
      { path: 'team/task-team', component: TaskTeamTableComponent },
      { path: 'task/import-log', component: ImportLogComponent },
    ],
  },
];
