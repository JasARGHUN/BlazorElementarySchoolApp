using BusinessLogic.Helpers;
using BusinessLogic.Repository.IRepository;
using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/employee";

        public EmployeeRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Create(Employee model)
        {
            var response = await _httpService.Post(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task Edit(Employee model)
        {
            var response = await _httpService.Put(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<Employee> GetById(int id)
        {
            return await _httpService.GetHelper<Employee>($"{url}/{id}");
        }

        public async Task<PaginatedResponseDTO<List<Employee>>> GetAll(PaginationDTO pagination)
        {
            return await _httpService.GetHelper<List<Employee>>(url, pagination);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<List<Employee>> GetObjectByName(string name)
        {
            var response = await _httpService.Get<List<Employee>>($"{url}/search/{name}");

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
