import { IComment } from "./comment";

export interface ITaskDetail {
  taskName: string;
  status: number;
  taskDescription: string;
  comments: IComment[];
  dueDate: Date;
  assignedTo: string[];
}
