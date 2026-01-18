import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from '../../core/ToastServices/toast.service';
import { ICategory } from '../../models/feature';
import { Severity } from '../../models/severity';
import { SideBarMenuService } from '../../services/side-bar-menu.service';
import { IMenuItem } from '../../models/menuItem';

@Component({
  selector: 'app-side-bar-menu',
  standalone: false,
  templateUrl: './side-bar-menu.component.html',
  styleUrls: ['./side-bar-menu.component.css'],
})
export class SideBarMenuComponent implements OnInit {
  menuItems: IMenuItem[] = [];
  @Input() isSideBarCollapsed: boolean = false;

  constructor(
    private readonly _router: Router,
    private readonly _menu: SideBarMenuService,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.fetchMenuItems();
  }

  private fetchMenuItems(): void {
    this._menu.fetchFeatures().subscribe({
      next: (categories: ICategory[]) => {
        this.menuItems = categories.map((category) => ({
          name: category.category,
          icon: category.icon,
          subMenuOpen: false,
          subItems: category.features.map((feature) => ({
            name: feature.featureName,
            route: feature.pathUrl,
          })),
        }));
      },
      error: (error) => {
        this._toastService.showToast(
          Severity.error,
          'Error fetching sidebar data:',
          error.message
        );
      },
    });
  }

  public toggleSubMenu(item: IMenuItem): void {
    item.subMenuOpen = !item.subMenuOpen;
  }

  public navigateTo(route: string): void {
    this._router.navigate([route]);
  }
}
