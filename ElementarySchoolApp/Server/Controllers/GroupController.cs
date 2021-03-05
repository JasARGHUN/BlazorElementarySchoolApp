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
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _storageService;
        private readonly IMapper _mapper;

        public GroupController(ApplicationDbContext context, IFileStorageService storageService, IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post (Group model)
        {
            if (!string.IsNullOrWhiteSpace(model.GroupImage))
            {
                var modelImg = Convert.FromBase64String(model.GroupImage);
                model.GroupImage = await _storageService.SaveFile(modelImg, "jpg", "groupimages");
            }

            _context.Groups.Add(model);
            await _context.SaveChangesAsync();

            return model.Id;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _context.Groups.FirstOrDefaultAsync(x => x.Id == Id);

            if (model == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(model.GroupImage))
            {
                await _storageService.DeleteFile(model.GroupImage, "groupimages"); // ?
            }

            _context.Remove(model);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Group model)
        {
            var obj = await _context.Groups.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (obj == null)
            {
                return NotFound();
            }

            obj = _mapper.Map(model, obj);

            if (!string.IsNullOrWhiteSpace(model.GroupImage))
            {
                var modelImage = Convert.FromBase64String(model.GroupImage);
                obj.GroupImage = await _storageService.EditFile(modelImage, "jpg", "groupimages", obj.GroupImage);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetById(int id)
        {
            var model = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpGet]
        public async Task<ActionResult<List<Group>>> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Groups.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.ItemPerPage);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<Group>>> FilterByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Group>();
            }

            return await _context.Groups.Where(x => x.Name.Contains(searchText))
                                           .Take(10).ToListAsync();
        }
    }
}
