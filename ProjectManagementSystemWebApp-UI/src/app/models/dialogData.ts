import { IColumn } from "./column";

export interface IDialogData<T> {
  entityType: 'Project' | 'Task' | 'Team';
  data: T;
  columns?: IColumn[];
}
