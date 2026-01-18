export interface ICategory {
  id: number;
  category: string;
  icon: string;
  features: IFeature[];
}

export interface IFeature {
  id: number;
  categoryId: number;
  featureName: string;
  pathUrl: string;
}
