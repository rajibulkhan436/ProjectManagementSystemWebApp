export interface IFailedImport {
  id: number | null;
  importId: number;
  taskName: string;
  failureReason: string;
}
