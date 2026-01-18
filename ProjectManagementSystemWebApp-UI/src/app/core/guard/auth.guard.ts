import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Severity } from '../../models/severity';
import { SessionHelperService } from '../session-helper.service';
import { ToastService } from '../ToastServices/toast.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private readonly _sessionService: SessionHelperService,
    private readonly _router: Router,
    private readonly _toastService: ToastService
  ) {}

  canActivate(): boolean {
    if (this._sessionService.isLoggedIn()) {
      return true;
    } else {
      this._router.navigate(['/login']);
      this._toastService.showToast(
        Severity.error,
        'Kindly login First',
        'Unauthorized Access'
      );
      return false;
    }
  }
}
