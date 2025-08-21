namespace WebChat.Models;

public class PaginatedData<T>
{
    public int TotalCount { get; set; }
    public List<T> Data { get; set; } = new List<T>();
}
