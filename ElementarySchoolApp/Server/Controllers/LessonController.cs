using BusinessLogic.Data;
using ElementarySchoolApp.Server.Helpers;
using ElementarySchoolApp.Shared.DTO;
using ElementarySchoolApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Lesson model)
        {
            _context.Lessons.Add(model);
            await _context.SaveChangesAsync();

            return model.Id;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Lessons.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.ItemPerPage);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == Id);

            if(model == null)
            {
                return NotFound();
            }

            _context.Remove(model);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<Lesson>>> FilterByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Lesson>();
            }

            return await _context.Lessons.Where(x => x.LessonName.Contains(searchText))
                                           .Take(10).ToListAsync();
        }
    }
}
