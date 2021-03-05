namespace ElementarySchoolApp.Shared.DTO
{
    public class FilterItemDTO
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;

        public PaginationDTO Pagination
        {
            get
            {
                return new PaginationDTO()
                {
                    Page = Page,
                    ItemPerPage = ItemsPerPage
                };
            }
        }

        public string Name { get; set; }
        public int GroupId { get; set; }
        public bool IsCompleted { get; set; }
        public bool UpcomingReleases { get; set; }
    }
}
