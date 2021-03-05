using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IRoomRepository
    {
        Task<int> Create(Room model);
        Task Update(Room model);
        Task<RoomEditDTO> GetById(int id);
        Task<List<Room>> GetObjectByName(string name);
        Task<RoomDetailsDTO> GetObjectDetails(int id);
        //Task<RoomEditDTO> GetObjectForUpdate(int id);
        Task<IndexPageDTO> IndexPageDTO();
        Task DeleteAsync(int id);
        Task<PaginatedResponseDTO<List<Room>>> GetItemsFiltered(FilterItemDTO filterItem);
    }
}
