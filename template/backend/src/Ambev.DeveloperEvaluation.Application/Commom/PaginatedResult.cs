namespace Ambev.DeveloperEvaluation.Application.Commom
{
    public class PaginatedResult<T>
    {
        public List<T> Results { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public PaginatedResult(List<T> results, int totalCount, int currentPage, int pageSize)
        {
            Results = results;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
