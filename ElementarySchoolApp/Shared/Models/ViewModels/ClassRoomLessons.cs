using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Shared.Models.ViewModels
{
    public class ClassRoomLessons
    {
        public int ClassRoomLessonsId { get; set; }
        [ForeignKey("ClassRoomLessonsId")]
        public Room Room { get; set; }

        public int LessonId { get; set; }
        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }
    }
}
