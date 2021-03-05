using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IGroupRepository
    {
        Task CreateGroup(Group model);
        Task Edit(Group model);
        Task<PaginatedResponseDTO<List<Group>>> GetAll(PaginationDTO pagination);
        Task<Group> GetById(int id);
        Task<List<Group>> GetObjectByName(string name);
        Task DeleteAsync(int id);
        Task<List<Group>> GetItemList();
    }
}
