export interface IColumn {
  field: string;
  headerName: string;
  type: string;
  onClick?: (rowData: any) => void;
}
