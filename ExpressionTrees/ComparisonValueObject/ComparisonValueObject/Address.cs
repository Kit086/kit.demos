namespace ComparisonValueObject;

public class Address //: ValueObject
{
    public Address()
    {
    }

    public Address(string country, string province, string city, string detail)
    {
        Country = country;
        Province = province;
        City = city;
        Detail = detail;
    }

    public string Country { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Detail { get; set; } = null!;

    #region overload EqualOperator

    // protected override IEnumerable<object> GetEqualityComponents()
    // {
    //     yield return Country;
    //     yield return Province;
    //     yield return City;
    //     yield return Detail;
    // }

    #endregion
}