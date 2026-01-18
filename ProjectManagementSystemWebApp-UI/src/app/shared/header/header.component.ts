import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { SessionHelperService } from '../../core/session-helper.service';
import { ThemeService } from '../../core/themes/ThemeService/theme.service';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  @Output() sidebarToggle = new EventEmitter<boolean>();
  isLoginDropDownOpen = false;
  isDarkMode: boolean = false;
  title = 'ProjectManagementSystem';
  userName: string = '';
  isSideBarCollapsed = false;
  mode: string = 'Light Mode';

  constructor(
    private readonly _router: Router,
    private readonly _sessionService: SessionHelperService,
    private readonly _themeService: ThemeService
  ) {
    this._themeService.isDarkMode.subscribe((isDark) => {
      this.isDarkMode = isDark;
      this.mode = this.isDarkMode ? 'Dark Mode' : 'Light Mode';
    });
  }

  ngOnInit(): void {
    this.setUserProfile();
  }

  private setUserProfile(): void {
    this.userName = this._sessionService.get('userName') ?? 'user';
  }
  toggleDropdown(): void {
    this.isLoginDropDownOpen = !this.isLoginDropDownOpen;
  }

  logout(): void {
    this._sessionService.deleteSession();
    this._router.navigate(['/login']);
  }

  toggleDarkMode(): void {
    this._themeService.toggleDarkMode();
  }

  toggleSidebar(): void {
    this.isSideBarCollapsed = !this.isSideBarCollapsed;
    this.sidebarToggle.emit(this.isSideBarCollapsed);
  }
}
