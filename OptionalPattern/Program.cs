using OptionalPattern;

#region OptionalPattern:

// Person mann = Person.Create("Thomas", "Mann");
// Person aristotle = Person.Create("Aristotle");
// Person austen = Person.Create("Jane", "Austen");
// Person asimov = Person.Create("Isaac", "Asimov");
// Person marukami = Person.Create("Haruki", "Murakami");

// Book faustus = Book.Create("Doctor Faustus", mann);
// Book rhetoric = Book.Create("Rhetoric", aristotle);
// Book nights = Book.Create("One Thousand and One Nights");
// Book foundation = Book.Create("Foundation", asimov);
// Book robots = Book.Create("I, Robot", asimov);
// Book pride = Book.Create("Pride and Prejudice", austen);
// Book mahabharata = Book.Create("Mahabharata");
// Book windup = Book.Create("Windup Bird Chronicle", marukami);

// IEnumerable<Book> library = new[] { faustus, rhetoric, nights, foundation, robots, pride, mahabharata, windup };

// var bookshelf = library
//     .GroupBy(GetAuthorInitial)
//     .OrderBy(group => group.Key.Reduce(string.Empty));

// foreach (var group in bookshelf)
// {
//     string header = group.Key.Map(initial => $"[ {initial} ]").Reduce("[   ]");
//     foreach (var book in group)
//     {
//         Console.WriteLine($"{header} -> {book}");
//         header = "     ";
//     }
// }

// Console.WriteLine(new string('-', 40));

// var authorNameLengths = library
//     .GroupBy(GetAuthorNameLength)
//     .OrderBy(group => group.Key.Reduce(0));

// foreach (var group in authorNameLengths)
// {
//     string header = group.Key.Map(length => $"[ {length,2} ]").Reduce("[    ]");
//     foreach (var book in group)
//     {
//         Console.WriteLine($"{header} -> {book}");
//         header = "      ";
//     }
// }

// ValueOption<int> GetAuthorNameLength(Book book) =>
//     book.Author.Map(GetName).MapValue(s => s.Length);

// string GetName(Person person) =>
//     person.LastName
//         .Map(lastName => $"{person.FirstName} {lastName}")
//         .Reduce(person.FirstName);

// Option<string> GetAuthorInitial(Book book)
// {
//     return book.Author.MapOptional(GetPersonInitial);
// }

// Option<string> GetPersonInitial(Person person) =>
//     person.LastName
//         .MapValue(GetInitial)
//         .Reduce(() => GetInitial(person.FirstName));

// Option<string> GetInitial(string name) =>
//     name.WhereNot(string.IsNullOrWhiteSpace)
//         .Map(s => s.TrimStart().Substring(0, 1).ToUpper());

#endregion

#region Nullable:

NullablePerson mann = NullablePerson.Create("Thomas", "Mann");
NullablePerson aristotle = NullablePerson.Create("Aristotle");
NullablePerson austen = NullablePerson.Create("Jane", "Austen");
NullablePerson asimov = NullablePerson.Create("Isaac", "Asimov");
NullablePerson marukami = NullablePerson.Create("Haruki", "Murakami");

NullableBook faustus = NullableBook.Create("Doctor Faustus", mann);
NullableBook rhetoric = NullableBook.Create("Rhetoric", aristotle);
NullableBook nights = NullableBook.Create("One Thousand and One Nights");
NullableBook foundation = NullableBook.Create("Foundation", asimov);
NullableBook robots = NullableBook.Create("I, Robot", asimov);
NullableBook pride = NullableBook.Create("Pride and Prejudice", austen);
NullableBook mahabharata = NullableBook.Create("Mahabharata");
NullableBook windup = NullableBook.Create("Windup Bird Chronicle", marukami);

IEnumerable<NullableBook> library = new[] { faustus, rhetoric, nights, foundation, robots, pride, mahabharata, windup };

var author = GetAuthorInitial(rhetoric);

Console.WriteLine(author);

var bookshelf = library
    .GroupBy(GetAuthorInitial)
    .OrderBy(group => group.Key ?? string.Empty);

foreach (var group in bookshelf)
{
    string header = !string.IsNullOrWhiteSpace(group.Key)?  $"[ {group.Key} ]" : "[   ]";
    foreach (var book in group)
    {
        Console.WriteLine($"{header} -> {book}");
        header = "     ";
    }
}

Console.WriteLine(new string('-', 40));

var authorNameLengths = library
    .GroupBy(GetAuthorNameLength)
    .OrderBy(group => group.Key ?? 0);

foreach (var group in authorNameLengths)
{
    string header = group.Key is not null ? $"[ {group.Key,2} ]" : "[    ]";
    foreach (var book in group)
    {
        Console.WriteLine($"{header} -> {book}");
        header = "      ";
    }
}

int? GetAuthorNameLength(NullableBook book) =>
    book.Author is not null
        ? GetName(book.Author).Length
        : null;

string GetName(NullablePerson person) =>
    person.LastName is not null
        ? $"{person.FirstName} {person.LastName}"
        : person.FirstName;

string? GetAuthorInitial(NullableBook book) =>
    book.Author is not null && !string.IsNullOrWhiteSpace(book.Author.LastName)
        ? GetPersonInitial(book.Author)
        : book.Author is not null && !string.IsNullOrWhiteSpace(book.Author.FirstName)
            ? GetPersonInitial(book.Author)
            : null;

string? GetPersonInitial(NullablePerson person) =>
    !string.IsNullOrWhiteSpace(person.LastName)
        ? GetInitial(person.LastName)
        : GetInitial(person.FirstName);

string? GetInitial(string name) =>
    name?.TrimStart()?[..1]?.ToUpper();

#endregion