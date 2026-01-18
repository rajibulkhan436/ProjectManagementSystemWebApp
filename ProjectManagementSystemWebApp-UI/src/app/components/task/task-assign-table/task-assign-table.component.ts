import { Component } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { Severity } from '../../../models/severity';
import { ITask } from '../../../models/task';
import { ITaskAssignment } from '../../../models/task-assignment';
import { TaskService } from '../../../services/task.service';
import { TaskDialogComponent } from '../../task-dialog/task-dialog.component';
import { TaskDetailComponent } from '../task-detail/task-detail.component';

@Component({
  selector: 'app-task-assign-table',
  standalone: false,
  templateUrl: './task-assign-table.component.html',
  styleUrl: './task-assign-table.component.css',
  providers: [DialogService],
})
export class TaskAssignTableComponent {
  title: string = 'tasks';
  tasks: ITask[] = [];
  columns: IColumn[] = [
    { field: 'id', headerName: 'ID', type: 'text' },
    { field: 'taskName', headerName: 'Task Name', type: 'text' },
    { field: 'projectId', headerName: 'Project Id', type: 'text' },
    {
      field: 'taskDescription',
      headerName: 'Task Description',
      type: 'longtext',
    },
    { field: 'dueDate', headerName: 'Due Date', type: 'date' },
    { field: 'status', headerName: 'Status', type: 'status' },
    { field: 'assign', headerName: 'AssignMember', type: 'assign' }
  ];

  constructor(
    private readonly _taskService: TaskService,
    private readonly _toastService: ToastService,
    private readonly _dialogService: DialogService
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

  openTaskDialog(task: ITask): void {
    const ref = this._dialogService.open(TaskDialogComponent, {
      header: 'Assign Task',
      width: '40%',
      data: {
        entity: task,
        columns: this.columns.filter(
          (col) => !['action', 'status', 'date', 'comment'].includes(col.type)
        ),
      },
      modal: true,
    });

    ref.onClose.subscribe((newTaskAssigned: ITaskAssignment) => {
      if (newTaskAssigned) {
        this.onDialogClosed(newTaskAssigned);
      }
    });
  }

  onDialogClosed(newTaskAssigned: ITaskAssignment): void {
    this._taskService.assignTask(newTaskAssigned).subscribe({
      next: () => {
        this._toastService.showToast(
          Severity.success,
          'Task Assigned successfully!',
          'Your TaskAssignment has been saved.'
        );
        this.getTasks();
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error Assigning Task',
          error.message
        );
      }
    });
  }

  openTaskDetailDialog(task: ITask): void {
    this._dialogService.open(TaskDetailComponent, {
      header: 'Task Details',
      width: '60%',
      modal: true,
      data: { task }
    });
  }
}
