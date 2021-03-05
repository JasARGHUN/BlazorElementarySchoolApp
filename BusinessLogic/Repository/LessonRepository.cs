using BusinessLogic.Helpers;
using BusinessLogic.Repository.IRepository;
using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/lesson";

        public LessonRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Create(Lesson model)
        {
            var response = await _httpService.Post(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task Edit(Lesson model)
        {
            var response = await _httpService.Put(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<Lesson> GetById(int id)
        {
            return await _httpService.GetHelper<Lesson>($"{url}/{id}");
        }

        public async Task<PaginatedResponseDTO<List<Lesson>>> GetAll(PaginationDTO pagination)
        {
            return await _httpService.GetHelper<List<Lesson>>(url, pagination);
        }

        public async Task<List<Lesson>> GetItemList()
        {
            var response = await _httpService.Get<List<Lesson>>(url);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<List<Lesson>> GetObjectByName(string name)
        {
            var response = await _httpService.Get<List<Lesson>>($"{url}/search/{name}");

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
