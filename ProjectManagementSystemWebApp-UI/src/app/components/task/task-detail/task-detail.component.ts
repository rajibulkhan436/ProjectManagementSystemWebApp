import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { map, Observable } from 'rxjs';
import { SessionHelperService } from '../../../core/session-helper.service';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { getStatusText } from '../../../core/utils/task-status.utils';
import { IComment } from '../../../models/comment';
import { IOptions } from '../../../models/option';
import { Severity } from '../../../models/severity';
import { ITask } from '../../../models/task';
import { ITaskTeam } from '../../../models/task-team';
import { ITaskDetail } from '../../../models/taskDetail';
import { TeamService } from '../../../services/team.service';

@Component({
  selector: 'app-task-detail',
  standalone: false,
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css'],
})


export class TaskDetailComponent implements OnInit {
  selectedTask!: ITask;
  task!: ITaskDetail;
  commentForm!: FormGroup;
  statusForm!: FormGroup;
  newComment!: IComment;
  statusOptions: IOptions[] = [
      { label: 'Not Started', value: 1 },
      { label: 'In Progress', value: 2 },
      { label: 'Completed', value: 3 }
    ];

  constructor(
    private readonly _config: DynamicDialogConfig,
    private readonly _ref: DynamicDialogRef,
    private  readonly _fb: FormBuilder,
    private readonly _sessionService: SessionHelperService,
    private readonly _teamService: TeamService,
    private readonly _toastService: ToastService 
  ) {}

  ngOnInit(): void {
    this.initializeTaskDetail();
    this.initializeForms();
  }


  private initializeTaskDetail(): void {
    if (this._config.data.task) {
      this.selectedTask = this._config.data.task;
      this.task = {
        taskName: this.selectedTask.taskName,
        status: this.selectedTask.status,
        taskDescription: this.selectedTask.taskDescription,
        dueDate: this.selectedTask.dueDate,
        assignedTo: [],       
        comments: this.selectedTask.comments || [],
      };

      this.getTeamMembers(this.selectedTask.id).subscribe({
      next: (teamNames: string[]) => {
        this.task.assignedTo = teamNames;
      },
      error: (error) => {
        this._toastService.showToast(
                  Severity.error,
                  'Error fetching Team Members',
                  error.message
                );
      }
    });
    }
  }

  private initializeForms(): void {
    const userName = this._sessionService.get('userName') ?? 'Unknown';
    this.commentForm = this._fb.group({
      userName: [userName, Validators.required],
      commentMessage: ['', Validators.required],
    });

    this.statusForm = this._fb.group({
      status: [this.task.status, Validators.required],
    });
  }

  private getTeamMembers(taskId: number): Observable<string[]> {
  return this._teamService.fetchTaskTeamById(taskId).pipe(
    map((taskTeam: ITaskTeam[]) => taskTeam.map(member => member.teamMemberName))
  );
  }


  private getStatusText(status: number): string {
  return getStatusText(status);
  }


  get statusControl(): FormControl {
  return this.statusForm.get('status') as FormControl;
  }


  addComment(): void {
  if (this.commentForm.valid && this.selectedTask) {
    const userName = this._sessionService.get('userName') ?? 'Unknown';
    const userId = Number(this._sessionService.get('userId')) || 0;
    
    this.newComment = {
      id: 0,
      userId: userId,
      userName: userName,
      taskId: this.selectedTask.id,
      commentedAt: new Date(),
      commentMessage: this.commentForm.value.commentMessage,
    };

    this.task.comments.push(this.newComment);
    this.commentForm.reset({
      userName: userName, 
      commentMessage: ''
    });
  }
}

  saveChanges(): void {
    const updatedTask = {
      id: this.selectedTask.id,
      status: this.statusForm.value.status,
      comment: this.newComment,
    };

    this._ref.close(updatedTask);
    console.log(updatedTask);
  }

  closeDialog(): void {
    this._ref.close(null);
  }
}
