using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Shared.DTO
{
    public class RoomEditDTO
    {
        public Room Room { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Group> SelectedGroups { get; set; }
        public List<Group> NotSelectedGroups { get; set; }
        public List<Lesson> SelectedLessons { get; set; }
        public List<Lesson> NotSelectedLessons { get; set; }
    }
}
