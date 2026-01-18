import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { RouterModule } from '@angular/router';
import { AgGridAngular, AgGridModule } from 'ag-grid-angular';
import { NgToastModule } from 'ng-angular-popup';
import { MessageService } from 'primeng/api';
import { providePrimeNG } from 'primeng/config';
import { ToastModule } from 'primeng/toast';
import { AppComponent } from './app.component';
import { UserAuthModule } from './components/user-auth/user-auth.module';
import { MyPreset } from './core/presets/mypreset';
import { ToasterModule } from './core/ToastServices/toaster.module';
import { ProjectManagementModule } from './project-management.module';
import { ProgressSpinnerComponent } from './shared/progress-spinner/progress-spinner.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [AppComponent, ProgressSpinnerComponent],
  imports: [
    BrowserModule,
    RouterModule.forRoot([]),
    UserAuthModule,
    BrowserAnimationsModule,
    ProjectManagementModule,
    NgToastModule,
    SharedModule,
    ToasterModule,
    ToastModule,
    AgGridAngular,
    AgGridModule,
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi()),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: MyPreset,
        options: {
          darkModeSelector: '.my-app-dark',
        },
      },
    }),
    MessageService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
