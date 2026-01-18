using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Repositories
{
    public class ImportRepository: IImportRepository
    {
        private readonly DatabaseContext _dbContext;

        public ImportRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddImportLogAsync(Import import, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.Imports.AddAsync(import, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }catch(Exception exception)
            {
                throw new Exception($"New Import couldn't be added: {exception.Message}");
            }
        }

        public async Task AddTaskImportAsync(FailedImport failedImport, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.FailedImports.AddAsync(failedImport, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception) 
            {
                throw new Exception($"Failed import log couldn't be added {exception.Message}");
            }
        }

        public async Task<ICollection<Import>> GetImportsAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Imports
                    .Include(import => import.FailedImports)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Failed to retrieve imports: {exception.Message}");
            }
        }

        public async Task<ICollection<FailedImport>> GetTasksByImportIdAsync(int importId, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.FailedImports
                    .Where(failedImport => failedImport.ImportId == importId)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Failed to retrieve failed imports by import ID: {exception.Message}");
            }
        }

        public async  Task UpdateImportAsync(Import import, CancellationToken cancellationToken)
        {
            try
            {
                var requiredImport = await _dbContext.Imports.FindAsync(import.Id, cancellationToken);
                if (requiredImport != null)
                {
                    requiredImport.Total = import.Total;
                    requiredImport.Success = import.Success;
                    requiredImport.Fails = import.Fails;    
                }
                await _dbContext.SaveChangesAsync(cancellationToken);
            } catch (Exception exception) {
                throw new Exception($"Import Updation  couldn't be completed : {exception.Message}");
            }
        }
    }
}
