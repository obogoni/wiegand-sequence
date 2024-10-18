using System;
using FluentAssertions;

namespace Wiegand26.UnitTests;

public class LongToWiegandParserTests
{
    [Fact]
    public void Valid_long_value_is_parsed_correctly()
    {
        //Arrange

        ulong value = 10065535;

        //Act

        WiegandParser.TryParse(value, out var facilityCode, out var cardNumber);

        //Assert

        facilityCode.Should().Be(100);
        cardNumber.Should().Be(65535);
    }

    [Fact]
    public void Zero_long_value_is_parsed_correctly()
    {
        //Arrange

        ulong value = 00000000;

        //Act

        WiegandParser.TryParse(value, out var facilityCode, out var cardNumber);

        //Assert

        facilityCode.Should().Be(0);
        cardNumber.Should().Be(0);
    }
}
