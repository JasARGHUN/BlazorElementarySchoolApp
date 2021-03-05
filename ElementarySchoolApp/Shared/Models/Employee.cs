using System;
using ElementarySchoolApp.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElementarySchoolApp.Shared.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Photo { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Profile { get; set; }

        [Required]
        public DateTime? DateOfGotAJob { get; set; }

        public List<ClassRoomTeachers> ClassRoomTeachersList { get; set; } = new List<ClassRoomTeachers>();

        public override bool Equals(object obj)
        {
            if(obj is Employee item)
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
