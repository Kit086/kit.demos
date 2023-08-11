namespace BookManager;

public interface IBookRepository
{
    IReadOnlyCollection<Book> GetAllBooks();

    IReadOnlyCollection<Book> GetBooksByAuthor(string author);

    IReadOnlyCollection<Book> GetBooksByPublishedYearRange(int startYear, int endYear);

    Book? GetBookById(int id);

    void AddBook(Book book);

    void UpdateBook(Book book);

    void DeleteBook(int id);
}
