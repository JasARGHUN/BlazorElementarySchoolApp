using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Shared.Models.ViewModels
{
    public class ClassRoomTeachers
    {
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        
        public int ClassRoomTeachersId { get; set; }
        [ForeignKey("ClassRoomTeachersId")]
        public Room Room { get; set; }

        public int Order { get; set; }
    }
}
