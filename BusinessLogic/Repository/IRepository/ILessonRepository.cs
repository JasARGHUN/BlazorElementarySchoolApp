using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface ILessonRepository
    {
        Task Create(Lesson model);
        Task Edit(Lesson model);
        Task<PaginatedResponseDTO<List<Lesson>>> GetAll(PaginationDTO pagination);
        Task<Lesson> GetById(int id);
        Task<List<Lesson>> GetObjectByName(string name);
        Task DeleteAsync(int id);
        Task<List<Lesson>> GetItemList();
    }
}
