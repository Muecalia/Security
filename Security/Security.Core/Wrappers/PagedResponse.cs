namespace Security.Core.Wrappers
{
    public class PagedResponse<T>(List<T> datas, int pageNumber, int pageSize, int totalRecords, bool isSuccess, string message)
    {
        public List<T> Datas { get; set; } = datas;
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalPages { get; set; } = (int)Math.Ceiling((decimal)totalRecords / pageSize);
        public int TotalRecords { get; set; } = totalRecords;
        public bool IsSuccess { get; set; } = isSuccess;
        public string Message { get; set; } = message;

        public static PagedResponse<T> Error(string message) => new(default!, 0, 0, 0, false, message);

        public static PagedResponse<T> Success(List<T> datas, int pageNumber, int pageSize, int totalRecords, string message) => new(datas, pageNumber, pageSize, totalRecords, true, message);

    }
}
