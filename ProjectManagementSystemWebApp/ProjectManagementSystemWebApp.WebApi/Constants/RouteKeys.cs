namespace ProjectManagementSystemWebApp.WebApi.Constants
{
    public class RouteKeys
    {
        //Common Routes
        public const string MainRoute = "api/[controller]";
        public const string Default = "";

        //Project Routes
        public const string DisplayProjects = "DisplayProjects";
        public const string DisplayProjectsById = "DisplayProjectsById";
        public const string UpdateProjectStatus = "UpdateProjectStatus";
        public const string DisplayProjectReports = "DisplayProjectReports";
        public const string DeleteProject = "DeleteProject";

        //Task Routes
        public const string DisplayTasks = "DisplayTasks";
        public const string DisplayTasksById = "DisplayTasksById";
        public const string UpdateTaskStatus = "UpdateTaskStatus";
        public const string AssignTask = "AssignTask";
        public const string DeleteTask = "DeleteTask";
        public const string AddTask = "AddTask";

        //File Upload
        public const string UploadFile = "UploadFile";

        //Comment Routes
        public const string AddComment = "AddComment";

        //Team
        public const string DisplayTeamMembers = "DisplayTeamMembers";
        public const string DeleteTeamMember = "DeleteTeamMember";


        //TaskTeam Routes
        public const string DisplayTaskTeam = "DisplayTaskTeam";
        public const string DisplayTaskTeamById = "DisplayTaskTeamById";

        //Authentication Routes
        public const string Login = "Login";
        public const string Register = "Register";

        //Background Routes
        public const string EnqueueTask = "EnqueueTask";

        //Menu Routes
        public const string DisplayFeaturesMenu = "DisplayFeaturesMenu";

        //ImportLog Routes
        public const string DisplayImportLog = "DisplayImportLog";
    }
}
