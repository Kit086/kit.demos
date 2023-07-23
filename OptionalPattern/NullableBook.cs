namespace OptionalPattern;

public class NullableBook
{
    public string Title { get; }
    public NullablePerson? Author { get; }

    private NullableBook(string title, NullablePerson? author) =>
        (Title, Author) = (title, author);

    public static NullableBook Create(string title) =>
        new(title, null);

    public static NullableBook Create(string title, NullablePerson author) =>
        new(title, author);

    public override string ToString() =>
        this.Author is not null
            ? $"{Title} by {Author}"
            : Title;
}