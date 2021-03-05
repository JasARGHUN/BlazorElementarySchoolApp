using ElementarySchoolApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Shared.DTO
{
    public class IndexPageDTO
    {
        public List<Room> CompleteСlass { get; set; }
        public List<Room> UpcomingClass { get; set; }
    }
}
