using ElementarySchoolApp.Shared.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task TryRenewToken();
    }
}
