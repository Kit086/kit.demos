namespace BookManager.Tests.Unit;

public class BookServiceTests
{
    private readonly IBookRepository _bookRepository = Substitute.For<IBookRepository>(); // 为 IBookRepository 接口创建一个模拟对象

    private readonly BookService _sut; // System Under Test 被测试的系统

    private readonly List<Book> _books = new() // 创建一个用于测试的书籍列表
        {
            new Book(1, "The Lord of the Rings", "J.R.R. Tolkien", new DateTime(1954, 7, 29), new List<string>() { "Fantasy", "Adventure" }),
            new Book(2, "The Hobbit", "J.R.R. Tolkien", new DateTime(1937, 9, 21), new List<string>() { "Fantasy", "Children" }),
            new Book(3, "Harry Potter and the Philosopher's Stone", "J.K. Rowling", new DateTime(1997, 6, 26), new List<string>() { "Fantasy", "Young Adult" }),
            new Book(4, "Nineteen Eighty-Four", "George Orwell", new DateTime(1949, 6, 8), new List<string>() { "Dystopian", "Political" })
        };

    public BookServiceTests()
    {
        _sut = new(_bookRepository);
    }

    [Fact]
    public void GetBooksByAuthor_WithValidAuthorName_ReturnsMatchingBooks()
    {
        // Arrange
        var author = "J.R.R. Tolkien"; // 为测试定义作者名 author

        _bookRepository.GetBooksByAuthor(author).Returns(_books.Where(b => b.Author == author).ToList()); // 当调用 _bookRepository.GetBooksByAuthor 且传入参数为 author 时配置模拟对象返回预定义好的书籍列表 _books 中作者为 author 的书籍

        // Act
        var result = _sut.GetBooksByAuthor(author); // 调用参数为 author 的 GetBooksByAuthor 方法，并将返回值赋值给 result 变量

        // Assert
        result.Should().NotBeNull(); // 断言 result 不为 null
        result.Should().HaveCount(2); // 断言 result 包含两本书
        result.Should().OnlyContain(b => b.Author == author); // 断言 result 中的书籍作者都是 author
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetBooksByAuthor_WithNullOrEmptyAuthorName_ThrowsArgumentException(string author)
    {
        // Act and Assert
        _sut.Invoking(bs => bs.GetBooksByAuthor(author)) // 用 null 或空字符串为参数调用 GetBooksByAuthor 方法
            .Should().Throw<ArgumentException>() // 验证是否抛出 ArgumentException 异常
            .WithMessage("Author name cannot be null or empty"); // 验证异常消息是否正确
    }

    [Fact]
    public void GetBooksByPublishedYearRange_WithValidYearRange_ReturnsMatchingBooks()
    {
        // Arrange
        var startYear = 1950; // 为测试定义起始年份
        var endYear = 2000; // 为测试定义结束年份

        _bookRepository.GetBooksByPublishedYearRange(startYear, endYear).Returns(_books.Where(b => b.PublishedDate.Year >= startYear && b.PublishedDate.Year <= endYear).ToList()); // 当调用 _bookRepository.GetBooksByPublishedYearRange 且传入参数为 startYear 和 endYear 时配置模拟对象返回预定义好的书籍列表 _books 中出版年份在范围内的书籍

        // Act
        var result = _sut.GetBooksByPublishedYearRange(startYear, endYear); // 调用带有年份范围的 GetBooksByPublishedYearRange 方法

        // Assert
        result.Should().NotBeNull(); // 验证结果不为 null
        result.Should().HaveCount(2); // 验证结果有两本书
        result.Should().OnlyContain(b => b.PublishedDate.Year >= startYear && b.PublishedDate.Year <= endYear); /// 验证结果只包含出版年份在范围内的书籍
    }

    [Theory]
    [InlineData(-1, 2000)]
    [InlineData(-100, -50)]
    public void GetBooksByPublishedYearRange_WithNegativeStartYear_ThrowsArgumentException(int startYear, int endYear)
    {
        // Act and Assert
        _sut.Invoking(bs => bs.GetBooksByPublishedYearRange(startYear, endYear)) // 用负起始年份调用 GetBooksByPublishedYearRange 方法
            .Should().Throw<ArgumentException>() // 验证是否抛出 ArgumentException 异常
            .WithMessage("Year cannot be negative"); // 验证异常消息是否正确
    }

    [Theory]
    [InlineData(1950, -1)]
    [InlineData(-50, -100)]
    public void GetBooksByPublishedYearRange_WithNegativeEndYear_ThrowsArgumentException(int startYear, int endYear)
    {
        // Act and Assert
        _sut.Invoking(bs => bs.GetBooksByPublishedYearRange(startYear, endYear)) // 用负结束年份调用 GetBooksByPublishedYearRange 方法
            .Should().Throw<ArgumentException>() // 验证是否抛出 ArgumentException 异常
            .WithMessage("Year cannot be negative"); // 验证异常消息是否正确
    }

    [Theory]
    [InlineData(2000, 1950)]
    [InlineData(2023, 2022)]
    public void GetBooksByPublishedYearRange_WithStartYearGreaterThanEndYear_ThrowsArgumentException(int startYear, int endYear)
    {
        // Act and Assert
        _sut.Invoking(bs => bs.GetBooksByPublishedYearRange(startYear, endYear)) // 用起始年份大于结束年份调用 GetBooksByPublishedYearRange 方法
            .Should().Throw<ArgumentException>() // 验证是否抛出 ArgumentException 异常
            .WithMessage("Start year cannot be greater than end year"); // 验证异常消息是否正确
    }

    [Theory]
    [InlineData(1900, 1920)]
    [InlineData(2020, 2030)]
    public void GetBooksByPublishedYearRange_WithNoMatchingBooks_ReturnsEmptyList(int startYear, int endYear)
    {
        // Arrange
        _bookRepository.GetBooksByPublishedYearRange(Arg.Any<int>(), Arg.Any<int>()).Returns(Enumerable.Empty<Book>().ToList()); // 当调用 _bookRepository.GetAllBooks 且传入任意 int 类型参数时配置模拟对象返回预定义的书籍列表 _books

        // Act
        var result = _sut.GetBooksByPublishedYearRange(startYear, endYear); // 调用年份范围不匹配任何书籍的 GetBooksByPublishedYearRange 方法

        // Assert
        result.Should().NotBeNull(); // 验证结果不为 null
        result.Should().BeEmpty(); // 验证结果为空
    }
}
