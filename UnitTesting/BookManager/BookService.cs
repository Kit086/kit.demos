namespace BookManager;

public sealed class BookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IReadOnlyCollection<Book> GetBooksByAuthor(string author)
    {
        if (string.IsNullOrEmpty(author))
        {
            throw new ArgumentException("Author name cannot be null or empty");
        }

        return _bookRepository.GetBooksByAuthor(author);
    }

    public IReadOnlyCollection<Book> GetBooksByPublishedYearRange(int startYear, int endYear)
    {
        if (startYear < 0 || endYear < 0)
        {
            throw new ArgumentException("Year cannot be negative");
        }

        if (startYear > endYear)
        {
            throw new ArgumentException("Start year cannot be greater than end year");
        }

        return _bookRepository.GetBooksByPublishedYearRange(startYear, endYear);
    }
}
