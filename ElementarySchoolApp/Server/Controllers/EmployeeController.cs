using AutoMapper;
using BusinessLogic.Data;
using ElementarySchoolApp.Server.Helpers;
using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _storageService;
        private readonly IMapper _mapper;

        public EmployeeController(ApplicationDbContext context, IFileStorageService storageService, IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Employee model)
        {
            if (!string.IsNullOrWhiteSpace(model.Photo))
            {
                var modelImg = Convert.FromBase64String(model.Photo);
                model.Photo = await _storageService.SaveFile(modelImg, "jpg", "images");
            }

            _context.Employees.Add(model);
            await _context.SaveChangesAsync();

            return model.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Employee model)
        {
            var obj = await _context.Employees.FirstOrDefaultAsync(x => x.Id == model.Id);

            if(obj == null)
            {
                return NotFound();
            }

            obj = _mapper.Map(model, obj);

            if (!string.IsNullOrWhiteSpace(model.Photo))
            {
                var modelImage = Convert.FromBase64String(model.Photo);
                obj.Photo = await _storageService.EditFile(modelImage, "jpg", "images", obj.Photo);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var item = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll([FromQuery]PaginationDTO pagination)
        {
            var queryable = _context.Employees.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.ItemPerPage);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<Employee>>> FilterByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Employee>();
            }

            return await _context.Employees.Where(x => x.FirstName.Contains(searchText) 
                                                    || x.MiddleName.Contains(searchText) 
                                                    || x.LastName.Contains(searchText))
                                           .Take(10).ToListAsync();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);

            if (!string.IsNullOrWhiteSpace(model.Photo))
            {
                await _storageService.DeleteFile(model.Photo, "images");
            }

            if (model == null)
            {
                return NotFound();
            }

            _context.Remove(model);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
