import { TaskStatus } from "../../models/taskStatus";

export function getStatusText(status: number): string {
  switch (status) {
    case TaskStatus.NotStarted:
      return 'NotStarted';
    case TaskStatus.InProgress:
      return 'InProgress';
    case TaskStatus.Completed:
      return 'Completed';
    default:
      return 'Unknown';
  }
}

export function getStatusClass(status: number): string {
  switch (status) {
    case TaskStatus.NotStarted:
      return 'bg-danger bg-gradient';
    case TaskStatus.InProgress:
      return 'bg-warning text-dark bg-gradient';
    case TaskStatus.Completed:
      return 'bg-success bg-gradient';
    default:
      return 'bg-dark bg-gradient';
  }
}
