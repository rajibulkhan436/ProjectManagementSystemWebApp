export interface IComment {
  id: number | null;
  userId:number;
  userName: string;
  taskId: number;
  commentedAt: Date;
  commentMessage?: string
}
