using FluentAssertions;
using CSharpFunctionalExtensions;

namespace Wiegand26.UnitTests;

public class SequenceTests
{
    [Fact]
    public void Card_from_new_sequence_is_created()
    {
        //Arrange

        var expectedCard = Card.From(0, 1);
        var sut = Sequence.NewSequence();

        //Act

        var result = sut.Next();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedCard);
    }

    [Fact]
    public void Card_from_existing_sequence_is_created()
    {
        //Arrange

        var expectedCard = Card.From(100, 11);
        var sut = Sequence.From(10000010, 25565535).Value;

        //Act

        var result = sut.Next();

        //Assert

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedCard);
    }

    [Fact]
    public void Facility_code_is_incremented_when_the_sequence_runs_out_of_card_numbers()
    {
        //Arrange

        var expectedCard = Card.From(101, 0);
        var sut = Sequence.From(10065535, 25565535).Value;

        //Act

        var result = sut.Next();

        //Assert

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedCard);
    }

    [Fact]
    public void Sequence_runs_out_of_card_numbers()
    {
        //Arrange

        var expectedCard = Card.From(101, 0);
        var sut = Sequence.From(25565535, 25565535).Value;

        //Act

        var result = sut.Next();

        //Assert

        result.IsFailure.Should().BeTrue();
    }

}