namespace OptionalPattern;

public class NullablePerson
{
    public string FirstName { get; }
    public string? LastName { get; }

    private NullablePerson(string firstName, string? lastName) =>
        (FirstName, LastName) = (firstName, lastName);

    public static NullablePerson Create(string firstName) =>
        new(firstName, null);

    public static NullablePerson Create(string firstName, string lastName) =>
        new(firstName, lastName);

    public override string ToString() =>
        !string.IsNullOrWhiteSpace(this.LastName)
            ? $"{FirstName} {LastName}"
            : FirstName;
}