export interface IProjectReport {
  projectName: string;
  taskName: string | null;
  taskStatus: number;
  dueDate: Date | null;
  projectStatus: number;
}
