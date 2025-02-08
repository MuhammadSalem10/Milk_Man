

namespace MilkMan.Shared.Parameters
{
    public class ProductQueryParameters
    {
        public int CategoryId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = true;
    }

}
