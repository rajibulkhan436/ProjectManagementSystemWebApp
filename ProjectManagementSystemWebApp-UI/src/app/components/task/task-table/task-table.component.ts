import { Component, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { IColumn } from '../../../models/column';
import { Severity } from '../../../models/severity';
import { ITask } from '../../../models/task';
import { TaskService } from '../../../services/task.service';
import { TaskDetailComponent } from '../task-detail/task-detail.component';

@Component({
  selector: 'app-task-table',
  standalone: false,
  templateUrl: './task-table.component.html',
  styleUrl: './task-table.component.css',
  providers: [DialogService],
})
export class TaskTableComponent implements OnInit {
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
    //{ field: 'details', headerName: 'Detail', type: 'detail' },
    { field: 'status', headerName: 'Status', type: 'status' }
  ];

  constructor(
    private readonly _dialogService: DialogService,
    private readonly _taskService: TaskService,
    private readonly _toastService: ToastService
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
      }
    });
  }

  openTaskDetailDialog(task: ITask): void {
    this._dialogService.open(TaskDetailComponent, {
      header: 'Task Details',
      width: '60%',
      modal: true,
      data:  {task} 
    });
  }

}
