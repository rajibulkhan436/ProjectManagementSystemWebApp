using ProjectManagementSystem.Services.Contracts;

namespace ProjectManagementSystem.Services.Services.Helper
{
    public class FileManager : IFileManager
    {
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
