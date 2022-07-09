using AAADemo.Domain.Exceptions;
using AAADemo.Domain.ValueObjects;
using FluentAssertions;

namespace AAADemo.Domain.UnitTests.ValueObjects;

public class ColorTests
{
    [Fact]
    public void From_ShouldReturnCorrectColorCode()
    {
        var code = "#FFFFFF";

        var color = Color.From(code);

        color.Code.Should().Be(code);
    }

    [Fact]
    public void ToString_ShouldReturnCode()
    {
        var color = Color.White;

        color.ToString().Should().Be(color.Code);
    }

    [Fact]
    public void ExplicitConversion_ShouldPerformExplicitConversion_WhenGivenSupportedColorCode()
    {
        var color = (Color)"#FFFFFF";

        color.Should().Be(Color.White);
    }

    [Fact]
    public void From_ShouldThrowUnsupportedColorException_WhenGivenNotSupportedColorCode()
    {
        FluentActions.Invoking(() => Color.From("##FF33CC"))
            .Should().Throw<UnsupportedColorException>();
    }
}
