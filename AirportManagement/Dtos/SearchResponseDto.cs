using Microsoft.EntityFrameworkCore;

namespace AirportManagement.Dtos
{
    public class SearchResponseDto<T>
    {
        private SearchResponseDto(List<T> items, int pageNumber, int pageSize, int totalCount) {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public List<T> Items { get;}
        public int PageNumber { get; }
        public int PageSize{ get; }
        public int TotalCount { get; }
        public bool HasNextPage => PageNumber * PageSize < TotalCount;
        public bool HasPreviousPage => PageNumber > 1;
        public static async Task<SearchResponseDto<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new(items, page, pageSize, totalCount);
        }

    }
}
