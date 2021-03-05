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
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _storageService;
        private readonly IMapper _mapper;

        public RoomController(ApplicationDbContext context, IFileStorageService storageService, IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Room model)
        {
            if (!string.IsNullOrWhiteSpace(model.RoomImage))
            {
                var modelImg = Convert.FromBase64String(model.RoomImage);
                model.RoomImage = await _storageService.SaveFile(modelImg, "jpg", "roomimages");
            }

            if(model.ClassRoomTeachersList != null)
            {
                for(int i = 0; i < model.ClassRoomTeachersList.Count(); i++)
                {
                    model.ClassRoomTeachersList[i].Order = i + 1;
                }
            }

            _context.Rooms.Add(model);
            await _context.SaveChangesAsync();

            return model.Id;
        }

        [HttpGet("update/{id}")]
        public async Task<ActionResult<RoomEditDTO>> PutGet(int id)
        {
            var modelActionResult = await GetById(id);

            if (modelActionResult.Result is NotFoundResult)
            {
                return NotFound();
            }

            var modelDetailDTO = modelActionResult.Value;

            var selectedGroupsIds = modelDetailDTO.Groups.Select(x => x.Id).ToList();
            var notSelectedGroups = await _context.Groups.Where(x => !selectedGroupsIds.Contains(x.Id)).ToListAsync();

            var selectedLessonsIds = modelDetailDTO.Lessons.Select(x => x.Id).ToList();
            var notSelectedLessons = await _context.Lessons.Where(x => !selectedLessonsIds.Contains(x.Id)).ToListAsync();

            var model = new RoomEditDTO();

            model.Room = modelDetailDTO.Room;

            model.SelectedGroups = modelDetailDTO.Groups;
            model.NotSelectedGroups = notSelectedGroups;

            model.SelectedLessons = modelDetailDTO.Lessons;
            model.NotSelectedLessons = notSelectedLessons;

            model.Employees = modelDetailDTO.Employees;

            return model;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Room model)
        {
            var obj = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (obj == null)
            {
                return NotFound();
            }

            obj = _mapper.Map(model, obj);

            if (!string.IsNullOrWhiteSpace(model.RoomImage))
            {
                var modelImage = Convert.FromBase64String(model.RoomImage);
                obj.RoomImage = await _storageService.EditFile(modelImage, "jpg", "roomimages", obj.RoomImage);
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"delete from ClassRoomTeachers where ClassRoomTeachersId = {model.Id}; delete from ClassRoomGroups where ClassRoomGroupId = {model.Id}; delete from ClassRoomLessons where ClassRoomLessonsId = {model.Id};");
            
            if (model.ClassRoomTeachersList != null)
            {
                for (int i = 0; i < model.ClassRoomTeachersList.Count; i++)
                {
                    model.ClassRoomTeachersList[i].Order = i + 1;
                }
            }

            obj.ClassRoomTeachersList = model.ClassRoomTeachersList;
            obj.ClassRoomGroupList = model.ClassRoomGroupList;
            obj.ClassRoomLessonsList = model.ClassRoomLessonsList;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IndexPageDTO>> Get()
        {
            int limit = 6;

            var completeСlass = await _context.Rooms.Where(x => x.CompleteСlass)
                .Take(limit).OrderByDescending(x => x.EndOfTheSchoolYear).ToListAsync();

            var upcomingClass = await _context.Rooms.Where(x => x.CompleteСlass != true)
                .OrderBy(x => x.StartOfTheSchoolYear).Take(limit).ToListAsync();

            var response = new IndexPageDTO();

            response.CompleteСlass = completeСlass;
            response.UpcomingClass = upcomingClass;

            return response;
        }

        [Authorize(Roles = StaticDetails.Role_Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == Id);

            if (!string.IsNullOrWhiteSpace(model.RoomImage))
            {
                await _storageService.DeleteFile(model.RoomImage, "roomimages");
            }

            if (model == null)
            {
                return NotFound();
            }

            _context.Remove(model);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDetailsDTO>> GetById(int id)
        {
            var model = await _context.Rooms.Where(x => x.Id == id)
                .Include(x => x.ClassRoomGroupList).ThenInclude(x => x.Group)
                .Include(x => x.ClassRoomTeachersList).ThenInclude(x => x.Employee)
                .Include(x => x.ClassRoomLessonsList).ThenInclude(x => x.Lesson)
                .FirstOrDefaultAsync();

            if(model == null)
            {
                return NotFound();
            }

            model.ClassRoomTeachersList = model.ClassRoomTeachersList.OrderBy(x => x.Order).ToList();

            var obj = new RoomDetailsDTO();
            obj.Room = model;
            obj.Groups = model.ClassRoomGroupList.Select(x => x.Group).ToList();
            obj.Lessons = model.ClassRoomLessonsList.Select(x => x.Lesson).ToList();
            obj.Employees = model.ClassRoomTeachersList.Select(x =>
                new Employee
                {
                    Id = x.Employee.Id,
                    FirstName = x.Employee.FirstName,
                    MiddleName = x.Employee.MiddleName,
                    LastName = x.Employee.LastName,
                    Age = x.Employee.Age,
                    Profile = x.Employee.Profile,
                    DateOfGotAJob = x.Employee.DateOfGotAJob
                }).ToList();

            return obj;
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<Room>>> FilterByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Room>();
            }

            return await _context.Rooms.Where(x => x.ClassName.Contains(searchText))
                                           .Take(10).ToListAsync();
        }

        [HttpPost("filter")]
        public async Task<ActionResult<List<Room>>> Filter(FilterItemDTO filterItem)
        {
            var itemQueryable = _context.Rooms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterItem.Name))
            {
                itemQueryable = itemQueryable.Where(x => x.ClassName.Contains(filterItem.Name));
            }

            if (filterItem.IsCompleted)
            {
                itemQueryable = itemQueryable.Where(x => x.CompleteСlass);
            }

            if (filterItem.UpcomingReleases)
            {
                var today = DateTime.Today;
                itemQueryable = itemQueryable.Where(x => x.StartOfTheSchoolYear > today);
            }

            if(filterItem.GroupId != 0)
            {
                itemQueryable = itemQueryable.Where(x => x.ClassRoomGroupList.Select(i => i.GroupId)
                .Contains(filterItem.GroupId));
            }

            await HttpContext.InsertPaginationParametersInResponse(itemQueryable, filterItem.ItemsPerPage);

            var items = await itemQueryable.Paginate(filterItem.Pagination).ToListAsync();

            return items;
        }
    }
}
