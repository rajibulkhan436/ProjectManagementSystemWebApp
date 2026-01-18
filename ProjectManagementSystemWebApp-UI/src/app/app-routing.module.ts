import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/user-auth/login/login.component';
import { RegisterComponent } from './components/user-auth/register/register.component';
import { ProjectManagementComponent } from './project-management.component';
import { AuthGuard } from './core/guard/auth.guard';

const routes: Routes = [
  { path : 'login', component: LoginComponent},
  { path : '', component: ProjectManagementComponent},
  { path : 'register', component: RegisterComponent},
  {
    path: 'project-management', component: ProjectManagementComponent, canActivate: [AuthGuard], 
    loadChildren: () => import('./project-management.module').then(m => m.ProjectManagementModule) 
  },
  { path: '**', redirectTo: '/project-management' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
