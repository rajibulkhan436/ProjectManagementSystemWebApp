import { IComment } from './comment';
import { ITaskAssignment } from './task-assignment';

export interface ITask {
  id: number;
  taskName: string;
  projectId: number;
  taskDescription: string;
  status: number;
  dueDate: Date;
  comments: IComment[];
  taskAssignments: ITaskAssignment[];
}
