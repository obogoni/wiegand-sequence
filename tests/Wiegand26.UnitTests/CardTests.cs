using System;
using FluentAssertions;

namespace Wiegand26.UnitTests;

public class CardTests
{

    [Fact]
    public void Zero_access_code_is_converted_to_string_correctly()
    {
        //Arrange

        var sut = Card.ZeroCode;

        //Act

        var zeroCodeStr = sut.ToString();

        //Assert

        zeroCodeStr.Should().Be("00000000");
    }

    [Fact]
    public void Access_code_is_converted_to_string_correctly()
    {
        //Arrange

        var sut = Card.From(150, 1);

        //Act

        var codeStr = sut.ToString();

        //Assert  

        codeStr.Should().Be("15000001");
    }
}
