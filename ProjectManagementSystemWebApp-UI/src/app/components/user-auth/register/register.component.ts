import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Message } from '../../../core/constants/message';
import { ToastService } from '../../../core/ToastServices/toast.service';
import { Severity } from '../../../models/severity';
import { AuthHubService } from '../shared/auth-hub.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  showConfirmPassword: boolean = false;
  showRegisterPassword: boolean = false;

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _auth: AuthHubService,
    private readonly _router: Router,
    private readonly _toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  public matchPassword(group: FormGroup): ValidationErrors | null {
    return group.get('password')?.value === group.get('confirmPassword')?.value
      ? null
      : { mismatch: true };
  }

  public onRegister(): void {
    if (this.registerForm.valid) {
      const user = {
        ...this.registerForm.value,
      };
      this._auth.register(user).subscribe({
        next: (res) => this.handleRegistrationSuccess(),
        error: (err) => this.handleRegistrationError(err),
      });
    } else {
      this._toastService.showToast(
        Severity.error,
        Message.invalid,
        Message.registrationFormInvalid
      );
    }
  }

  private initializeForm(): void {
    this.registerForm = this._fb.group(
      {
        username: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
        role: ['', Validators.required],
      },
      { validator: this.matchPassword }
    );
  }

  private handleRegistrationSuccess(): void {
    this._toastService.showToast(
      Severity.success,
      Message.registrationSuccess,
      Message.redirectionToLogin
    );
    this._router.navigate(['/login']);
  }

  private handleRegistrationError(err: Error): void {
    this._toastService.showToast(
      Severity.error,
      Message.registrationFail,
      err.message || Message.registrationErrorMessage
    );
  }

  public goToLogin(): void {
    this._router.navigate(['/login']);
  }
}
