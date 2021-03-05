using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolApp.Shared.DTO
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        //After add from Helpers folder a new static file HttpContextExtensions, QueryableExtensions, and upgrade your controllers
        //After add from BusinessLogic/Helpers IHttpServiceExtensionMethods and add new dto class PaginatedResponseDTO
    }
}
