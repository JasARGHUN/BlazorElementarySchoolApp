using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Shared.Models.ViewModels
{
    public class ClassRoomGroup
    {
        public int ClassRoomGroupId { get; set; }
        [ForeignKey("ClassRoomGroupId")]
        public Room Room { get; set; }

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
