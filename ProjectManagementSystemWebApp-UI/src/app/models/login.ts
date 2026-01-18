export interface ILogin {
  id: number;
  userName: string | null;
  email: string;
  password: string;
  role: string | null;
  token: string | null;
}
