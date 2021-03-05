using AutoMapper;
using ElementarySchoolApp.Shared.Models;

namespace ElementarySchoolApp.Server.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, Employee>().ForMember(x => x.Photo, options => options.Ignore());
            CreateMap<Group, Group>().ForMember(x => x.GroupImage, options => options.Ignore());
            CreateMap<Room, Room>().ForMember(x => x.RoomImage, options => options.Ignore());
        }
    }
}
