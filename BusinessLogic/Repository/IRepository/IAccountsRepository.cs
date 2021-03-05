using ElementarySchoolApp.Shared.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IAccountsRepository
    {
        Task<UserToken> Login(UserInfo userInfo);
        Task<UserToken> Register(UserInfo userInfo);
        Task<UserToken> RenewToken();
    }
}
