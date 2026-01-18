import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Routes } from '../../../core/constants/routes';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { Severity } from '../../../models/severity';
import { ITask } from '../../../models/task';
import { TaskService } from '../../../services/task.service';
import { ProgressSpinnerHubService } from '../../../shared/progress-spinner/shared/progress-spinner-hub.service';
import { DeleteDialogComponent } from '../../delete-dialog/delete-dialog.component';
import { UpdateDialogComponent } from '../../update-dialog/update-dialog.component';
import { TaskDetailComponent } from '../task-detail/task-detail.component';

@Component({
  selector: 'app-task-update-table',
  standalone: false,
  templateUrl: './task-update-table.component.html',
  styleUrl: './task-update-table.component.css',
  providers: [DialogService, ConfirmationService],
})
export class TaskUpdateTableComponent {
  title: string = 'tasks';
  tasks: ITask[] = [];
  showUploadDialog: boolean = false;
  uploadFileApi: string = Routes.uploadFile;
  columns: IColumn[] = [
    { field: 'id', headerName: 'ID', type: 'text' },
    { field: 'taskName', headerName: 'Task Name', type: 'text' },
    { field: 'projectId', headerName: 'Project Id', type: 'text' },
    {
      field: 'taskDescription',
      headerName: 'TaskDescription',
      type: 'longtext',
    },
    { field: 'dueDate', headerName: 'Due Date', type: 'date' },
    { field: 'status', headerName: 'Status', type: 'status' },
    {
      field: 'actions',
      headerName: 'Action',
      type: 'action',
      onClick: (rowData: ITask) => this.openUpdateDialog(rowData),
    },
  ];

  constructor(
    private readonly _taskService: TaskService,
    private readonly _toastService: ToastService,
    private readonly _dialogService: DialogService,
    private readonly _router: Router,
    private readonly _progressService: ProgressSpinnerHubService,
    private readonly _confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.getTasks();
  }

  private getTasks(): void {
    this._taskService.fetchTasks().subscribe({
      next: (tasks: ITask[]) => {
        this.tasks = tasks;
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error fetching tasks:',
          error.message
        );
      },
    });
  }

  openUpdateDialog(task: ITask): void {
    const ref = this._dialogService.open(UpdateDialogComponent, {
      header: 'Update Task',
      width: '40%',
      data: {
        entity: task,
        columns: this.columns
          .filter((col) => col.field !== 'actions')
          .map((col) =>
            col.field === 'details' ? { ...col, headerName: 'Comments' } : col
          ),
      },
      modal: true,
    });

    ref.onClose.subscribe((updatedTask: ITask) => {
      if (updatedTask) {
        this.onDialogClosed(updatedTask);
      }
    });
  }

  onDialogClosed(updatedTask: ITask): void {
    this._taskService.updateTask(updatedTask).subscribe({
      next: () => {
        this._toastService.showToast(
          Severity.success,
          'Task updated successfully!',
          'Your changes have been saved.'
        );
        this.getTasks();
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error updating Task',
          error.message
        );
      },
    });
  }

  openDeleteDialog(task: ITask): void {
    const ref = this._dialogService.open(DeleteDialogComponent, {
      header: 'Confirm Deletion',
      width: '30%',
      data: {
        message: `Are you sure you want to delete "${task.taskName}"?`,
        icon: 'pi pi-trash',
      },
      modal: true,
    });

    ref.onClose.subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.deleteRecord(task);
      }
    });
  }

  deleteRecord(task: ITask): void {
    this._taskService.deleteTask(task.id).subscribe({
      next: () => {
        this._toastService.showToast(
          Severity.success,
          'Task deleted successfully!',
          `Task "${task.taskName}" has been removed.`
        );
        this.getTasks();
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error deleting project:',
          error.message
        );
      },
    });
  }

  openTaskDetailDialog(task: ITask): void {
    const ref = this._dialogService.open(TaskDetailComponent, {
      header: 'Task Details',
      width: '60%',
      modal: true,
      data: { task },
    });

    ref.onClose.subscribe((updatedTask: ITask) => {
      if (updatedTask) {
        this.onDialogClosed(updatedTask);
      }
    });
  }

  openAddTaskDialog(): void {
    const additionalColumns: IColumn[] = [
      { field: 'comment', headerName: 'Comment', type: 'comment' },
      { field: 'assign', headerName: 'AssignMember', type: 'assign' },
    ];

    const ref = this._dialogService.open(UpdateDialogComponent, {
      header: 'Add Task',
      width: '40%',
      data: {
        columns: [
          ...this.columns.filter(
            (col) => col.field !== 'actions' && col.field !== 'id'
          ),
          ...additionalColumns,
        ],
      },
      modal: true,
    });

    ref.onClose.subscribe((updatedTask: ITask) => {
      if (updatedTask) {
        this.onDialogClosed(updatedTask);
      }
    });
  }

  onUpload(): void {
    this._progressService.setProgress(0);
    this._progressService.showProgressBar();

    setTimeout(() => {
      this._progressService.setProgress(100);

      this._confirmationService.confirm({
        message: 'Excel file has been uploaded and data is updated.',
        header: 'Upload Successful',
        icon: 'pi pi-check-circle',
        acceptLabel: 'Go to Import Log',
        rejectVisible: false,
        accept: () => {
          this.openImportLog();
        },
      });
      this.getTasks();
    }, 5000);
  }

  onUploadError(): void {
    this._toastService.showToast(
      Severity.error,
      'Upload failed',
      'Please try again.'
    );
  }

  openImportLog(): void {
    this._router.navigate(['task/import-log']);
  }
}
