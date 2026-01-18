export interface IAction<T> {
  label: string;
  iconSrc: string;
  onClick: (rowData: T) => void;
}
