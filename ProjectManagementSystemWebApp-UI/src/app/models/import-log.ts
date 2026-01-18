import { IFailedImport } from "./failed-Import";

export interface IImportLog {
  id: number | null;
  fileName: string;
  uploadTime: Date| string;
  total: number;
  success: number;
  fails: number;
  failedImports: IFailedImport[];
}
