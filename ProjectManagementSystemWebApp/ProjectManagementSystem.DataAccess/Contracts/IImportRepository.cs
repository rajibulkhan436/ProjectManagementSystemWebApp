
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface IImportRepository
    {
        Task AddImportLogAsync(Import import,CancellationToken cancellationToken);

        Task AddTaskImportAsync(FailedImport failedImport,CancellationToken cancellationToken);

        Task UpdateImportAsync(Import import,CancellationToken cancellationToken);

        Task<ICollection<FailedImport>> GetTasksByImportIdAsync(int importId,CancellationToken cancellationToken);

        Task<ICollection<Import>> GetImportsAsync(CancellationToken cancellationToken);


    }
}
