import { Environment } from '../../../environments/environment';

export const Routes = {
  //projects
  displayProjectStatus: 'project/DisplayProjects',
  updateProjectStatus: 'project/UpdateProjectStatus',
  displayProjectReports: 'project/DisplayProjectReports',
  deleteProject: 'project/DeleteProject',

  //menu
  displayFeaturesMenu: 'menu/DisplayFeaturesMenu',

  //Task
  updateTaskStatus: 'task/UpdateTaskStatus',
  displayTaskStatus: 'task/DisplayTasks',
  deleteTask: 'task/DeleteTask',
  AssignTask: 'task/AssignTask',

  //Team
  displayTeamMember: 'team/DisplayTeamMembers',
  displayTaskTeam: 'team/DisplayTaskTeam',
  displayTaskTeamById: 'team/DisplayTaskTeamById',

  //uploadFile
  uploadFile: `${Environment.baseUrl}file/UploadFile`,

  //importLog
  displayImportLog: `log/DisplayImportLog`,
};
