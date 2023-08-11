namespace BookManager;

public sealed class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public List<string> Genres { get; set; } = new();

    public Book(int id, string title, string author, DateTime publishedDate, List<string> genres)
    {
        Id = id;
        Title = title;
        Author = author;
        PublishedDate = publishedDate;
        Genres = genres;
    }
}
