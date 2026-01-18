import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AgGridAngular, AgGridModule } from 'ag-grid-angular';
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { FileUploadModule } from 'primeng/fileupload';
import { ProgressBarModule } from 'primeng/progressbar';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { DeleteDialogComponent } from './components/delete-dialog/delete-dialog.component';
import { ProjectReportTableComponent } from './components/project/project-report-table/project-report-table.component';
import { ProjectTableComponent } from './components/project/project-table/project-table.component';
import { ProjectUpdateTableComponent } from './components/project/project-update-table/project-update-table.component';
import { TaskDialogComponent } from './components/task-dialog/task-dialog.component';
import { DetailedLogComponent } from './components/task/detailed-log/detailed-log.component';
import { ImportLogComponent } from './components/task/import-log/import-log.component';
import { TaskAssignTableComponent } from './components/task/task-assign-table/task-assign-table.component';
import { TaskDetailComponent } from './components/task/task-detail/task-detail.component';
import { TaskTableComponent } from './components/task/task-table/task-table.component';
import { TaskUpdateTableComponent } from './components/task/task-update-table/task-update-table.component';
import { TaskTeamTableComponent } from './components/team/task-team-table/task-team-table.component';
import { TeamMemberTableComponent } from './components/team/team-member-table/team-member-table.component';
import { UpdateDialogComponent } from './components/update-dialog/update-dialog.component';
import { ValueCheckDirective } from './directives/value-check.directive';
import { ParseStatusPipe } from './pipes/parse-status.pipe';
import { StatusClassPipe } from './pipes/status-class.pipe';
import { ProjectManagementComponent } from './project-management.component';
import { ProjectManagementRoutes } from './project-management.route';
import { HeaderComponent } from './shared/header/header.component';
import { SharedModule } from './shared/shared.module';
import { SideBarMenuComponent } from './shared/side-bar-menu/side-bar-menu.component';

ModuleRegistry.registerModules([AllCommunityModule]);

@NgModule({
  declarations: [
    SideBarMenuComponent,
    ProjectManagementComponent,
    HeaderComponent,
    TaskTableComponent,
    TaskTeamTableComponent,
    ProjectReportTableComponent,
    ValueCheckDirective,
    ProjectTableComponent,
    UpdateDialogComponent,
    DeleteDialogComponent,
    ProjectUpdateTableComponent,
    TaskUpdateTableComponent,
    TaskAssignTableComponent,
    TeamMemberTableComponent,
    TaskDialogComponent,
    TaskDetailComponent,
    StatusClassPipe,
    ImportLogComponent,
    DetailedLogComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ProjectManagementRoutes),
    ParseStatusPipe,
    ButtonModule,
    TableModule,
    TagModule,
    SharedModule,
    ProgressBarModule,
    ToastModule,
    ParseStatusPipe,
    FormsModule,
    ReactiveFormsModule,
    DynamicDialogModule,
    DialogModule,
    DatePickerModule,
    AgGridModule,
    AgGridAngular,
    DropdownModule,
    FileUploadModule,
    ConfirmDialogModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProjectManagementModule {}
