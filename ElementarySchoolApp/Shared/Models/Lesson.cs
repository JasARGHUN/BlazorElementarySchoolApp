using ElementarySchoolApp.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElementarySchoolApp.Shared.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        public string LessonName { get; set; } // название предмета.

        [Required]
        public int Duration { get; set; } // длительность предмета.
        public List<ClassRoomLessons> ClassRoomLessonsList { get; set; } = new List<ClassRoomLessons>();

        public override bool Equals(object obj)
        {
            if (obj is Lesson item)
            {
                return Id == item.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
