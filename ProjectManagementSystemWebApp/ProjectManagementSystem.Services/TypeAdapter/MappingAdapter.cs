using Mapster;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.DTOs.ProjectDTOs;

namespace ProjectManagementSystem.Services.TypeAdapter
{
    public class MappingAdapter : IRegister
    {       
        public void Register(TypeAdapterConfig config)
        { 
            config.ForType<Project, ProjectDto>().AfterMapping((source, target) =>
            {
                target.ProjectId = source.Id;
            });

            config.ForType<ProjectDto, Project>().AfterMapping((source, target) =>
            {
                target.Id = source.ProjectId;
            });

        }
    }
}
