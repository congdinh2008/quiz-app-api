namespace QuizApp.Core;

public class PaginatedResult<T>
{
    public PaginatedResult(int pageNumber, int pageSize, int totalCount, IEnumerable<T> items) : this(pageNumber, pageSize, totalCount, items.ToArray())
    {

    }

    public PaginatedResult(int pageNumber, int pageSize, int totalCount, T[] items)
    {
        if (pageNumber < 0)
        {
            throw new ArgumentOutOfRangeException($"Page number ({nameof(pageNumber)}) should be at least 1.");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException($"Page size ({nameof(pageSize)}) should be at least 1.");
        }

        if (totalCount < 0)
        {
            throw new ArgumentOutOfRangeException($"Total count ({nameof(totalCount)}) should not be negative.");
        }

        var totalPages = totalCount / (float)pageSize;

        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalPages);
        Items = items;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public T[] Items { get; set; }
}