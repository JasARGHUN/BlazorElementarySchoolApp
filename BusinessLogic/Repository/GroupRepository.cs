using BusinessLogic.Helpers;
using BusinessLogic.Repository.IRepository;
using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/group";

        public GroupRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateGroup(Group model)
        {
            var response = await _httpService.Post(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task Edit(Group model)
        {
            var response = await _httpService.Put(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<PaginatedResponseDTO<List<Group>>> GetAll(PaginationDTO pagination)
        {
            return await _httpService.GetHelper<List<Group>>(url, pagination);
        }

        public async Task<List<Group>> GetItemList()
        {
            var response = await _httpService.Get<List<Group>>(url);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<Group> GetById(int id)
        {
            return await _httpService.GetHelper<Group>($"{url}/{id}");
        }

        public async Task<List<Group>> GetObjectByName(string name)
        {
            var response = await _httpService.Get<List<Group>>($"{url}/search/{name}");

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
