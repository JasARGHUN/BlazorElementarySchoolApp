using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Shared.DTO
{
    public class RoomDetailsDTO
    {
        public Room Room { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Group> Groups { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
