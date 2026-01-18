import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Environment } from '../../environments/environment';
import { Routes } from '../core/constants/routes';
import { HttpClientService } from '../core/http-client.service';
import { ITaskTeam } from '../models/task-team';
import { ITeamMember } from '../models/team-member';

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  private readonly _apiUrl = Environment.baseUrl;
  constructor(private readonly _http: HttpClientService) {}

  fetchTaskTeams(): Observable<ITaskTeam[]> {
    return this._http.get<ITaskTeam[]>(Routes.displayTaskTeam);
  }

  fetchTeamMembers(): Observable<ITeamMember[]>{
    return this._http.get<ITeamMember[]>(Routes.displayTeamMember);
  }

  fetchTaskTeamById(taskId: number): Observable<ITaskTeam[]>{
    return this._http.get<ITaskTeam[]>(Routes.displayTaskTeamById, {taskId});
  }
  
}
