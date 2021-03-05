using ElementarySchoolApp.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElementarySchoolApp.Shared.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string GroupImage { get; set; }

        [Required]
        public string Description { get; set; }

        public List<ClassRoomGroup> ClassRoomGroupList { get; set; } = new List<ClassRoomGroup>();

        public override bool Equals(object obj)
        {
            if (obj is Group item)
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
