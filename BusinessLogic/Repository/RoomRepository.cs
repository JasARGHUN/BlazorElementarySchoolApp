using BusinessLogic.Helpers;
using BusinessLogic.Repository.IRepository;
using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/room";

        public RoomRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> Create(Room model)
        {
            var response = await _httpService.Post<Room, int>(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task Update(Room model)
        {
            var response = await _httpService.Put(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<RoomEditDTO> GetById(int id)
        {
            return await _httpService.GetHelper<RoomEditDTO>($"{url}/update/{id}");
        }

        public async Task<IndexPageDTO> IndexPageDTO()
        {
            return await _httpService.GetHelper<IndexPageDTO>(url);
        }

        public async Task<RoomDetailsDTO> GetObjectDetails(int id)
        {
            return await _httpService.GetHelper<RoomDetailsDTO>($"{url}/{id}");
        }

        public async Task<List<Room>> GetObjectByName(string name)
        {
            var response = await _httpService.Get<List<Room>>($"{url}/search/{name}");

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

        public async Task<PaginatedResponseDTO<List<Room>>> GetItemsFiltered(FilterItemDTO filterItem)
        {
            var responseHTTP = await _httpService.Post<FilterItemDTO, List<Room>>($"{url}/filter", filterItem);
            var totalAmountPages = int.Parse(responseHTTP.HttpResponseMessage.Headers.GetValues("totalAmountPages").FirstOrDefault());

            var paginatedResponse = new PaginatedResponseDTO<List<Room>>()
            {
                Response = responseHTTP.Response,
                TotalAmountPages = totalAmountPages
            };

            return paginatedResponse;
        }
    }
}
