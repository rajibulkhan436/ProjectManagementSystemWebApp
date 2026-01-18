import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Routes } from '../core/constants/routes';
import { HttpClientService } from '../core/http-client.service';
import { ITask } from '../models/task';
import { ITaskAssignment } from '../models/task-assignment';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  constructor(private readonly _httpClientService: HttpClientService) {}

  fetchTasks(): Observable<ITask[]> {
    return this._httpClientService.get<ITask[]>(Routes.displayTaskStatus);
  }

  updateTask(updatedTask: ITask): Observable<ITask> {
    return this._httpClientService.put<ITask>(
      Routes.updateTaskStatus,
      updatedTask
    );
  }
  deleteTask(id: number): Observable<void> {
    return this._httpClientService.delete<void>(
      `${Routes.deleteTask}?id=${id}`
    );
  }

  assignTask(newTaskAssigned: ITaskAssignment): Observable<ITaskAssignment> {
    return this._httpClientService.post<ITaskAssignment>(
      Routes.AssignTask,
      newTaskAssigned
    );
  }
}
