using ElementarySchoolApp.Shared.DTO;
using System.Linq;

namespace ElementarySchoolApp.Server.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.ItemPerPage)
                .Take(paginationDTO.ItemPerPage);
        }
    }
}
