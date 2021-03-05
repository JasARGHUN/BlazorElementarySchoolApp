using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task Create(Employee model);
        Task Edit(Employee model);
        Task<PaginatedResponseDTO<List<Employee>>> GetAll(PaginationDTO pagination);
        Task<Employee> GetById(int id);
        Task<List<Employee>> GetObjectByName(string name);
        Task DeleteAsync(int id);
    }
}
