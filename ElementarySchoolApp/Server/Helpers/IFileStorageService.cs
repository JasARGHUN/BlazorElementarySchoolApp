using System.Threading.Tasks;

namespace ElementarySchoolApp.Server.Helpers
{
    public interface IFileStorageService
    {
        Task<string> SaveFile(byte[] content, string extension, string containerName);
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute);
    }
}
