import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Routes } from '../core/constants/routes';
import { HttpClientService } from '../core/http-client.service';
import { IProject } from '../models/project';
import { IProjectReport } from '../models/project-report';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  constructor(private readonly _httpClientService: HttpClientService) {}

  fetchProjects(): Observable<IProject[]> {
    return this._httpClientService.get<IProject[]>(Routes.displayProjectStatus);
  }

  updateProject(updatedProject: IProject): Observable<IProject> {
    return this._httpClientService.put<IProject>(
      Routes.updateProjectStatus,
      updatedProject
    );
  }

  fetchProjectReports(): Observable<IProjectReport[]> {
    return this._httpClientService.get<IProjectReport[]>(
      Routes.displayProjectReports
    );
  }

  deleteProject(projectId: number): Observable<void> {
    return this._httpClientService.delete<void>(
      `${Routes.deleteProject}?projectId=${projectId}`
    );
  }
}
