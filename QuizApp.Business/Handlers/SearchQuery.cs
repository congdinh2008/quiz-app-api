namespace QuizApp.Business;

public class SearchQuery
{
    public string? Keyword { get; set; }
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; }
    public OrderDirection OrderDirection { get; set; }
}
 