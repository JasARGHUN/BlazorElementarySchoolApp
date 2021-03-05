using ElementarySchoolApp.Shared.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementarySchoolApp.Shared.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string ClassName { get; set; } //
        public int ClassNumber { get; set; } //
        // Realise when lessons start and when lessons end.
        public DateTime? StartOfTheSchoolYear { get; set; } //
        public DateTime? EndOfTheSchoolYear { get; set; } //
        public string RoomImage { get; set; } //
        public int TotalSeats { get; set; } //
        public string Description { get; set; } //
        public bool CompleteСlass { get; set; } //

        // Может для чего то пригодится
        [NotMapped]
        public string ClassNameSubString
        {
            get
            {
                if (string.IsNullOrEmpty(ClassName))
                {
                    return null;
                }
                if(ClassName.Length > 100)
                {
                    return ClassName.Substring(0, 100) + "...";
                }
                else
                {
                    return ClassName;
                }
            }
        }

        public List<ClassRoomLessons> ClassRoomLessonsList { get; set; } //= new List<ClassRoomLessons>();
        public List<ClassRoomGroup> ClassRoomGroupList { get; set; } //= new List<ClassRoomGroup>();
        public List<ClassRoomTeachers> ClassRoomTeachersList { get; set; } //= new List<ClassRoomTeachers>();
    }
}
