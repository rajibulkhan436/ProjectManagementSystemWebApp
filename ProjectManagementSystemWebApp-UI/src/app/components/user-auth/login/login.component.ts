import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SessionHelperService } from '../../../core/session-helper.service';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { ILogin } from '../../../models/login';
import { Severity } from '../../../models/severity';
import { AuthHubService } from '../shared/auth-hub.service';
import { Message } from '../../../core/constants/message';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  showDialog: boolean = false;
  showPassword: boolean = false;
  
    constructor(
    private readonly _fb: FormBuilder,
    private readonly _auth: AuthHubService,
    private readonly _sessionService: SessionHelperService,
    private readonly _router: Router,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      let credentials = {
        ...this.loginForm.value,
      };
      this._auth.login(credentials).subscribe({
        next: (response) => this.handleLoginSuccess(response),
        error: (err) => this.handleLoginError(err),
      });
    } else {
      this._toastService.showToast(
        Severity.error,
        Message.invalid,
        Message.loginFormInvalid
      );
    }
  }

  private initializeForm(): void {
    this.loginForm = this._fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  private handleLoginSuccess(response: ILogin): void {
    this._toastService.showToast(
      Severity.success,
      Message.loginSuccess,
      Message.welcomeBack
    );

    setTimeout(() => {
      this._sessionService.set('token', response.token);
      this._sessionService.set('userName', response.userName);
      this._sessionService.set('userId', response.id);

      this._router.navigate(['/project-management']);
    }, 500);
  }

  private handleLoginError(err: Error): void {
    this._toastService.showToast(
      Severity.error,
      Message.loginFail,
      err.message || Message.loginError
    );
  }

  goToRegister(): void {
    this._router.navigate(['/register']);
  }
}
