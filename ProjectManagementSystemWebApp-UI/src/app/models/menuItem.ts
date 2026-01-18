export interface IMenuItem {
  name: string;
  subMenuOpen: boolean;
  icon?: string;
  subItems: ISubItem[];
}
export interface ISubItem {
  name: string;
  route: string;
}
