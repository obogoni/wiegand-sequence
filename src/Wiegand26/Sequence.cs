using CSharpFunctionalExtensions;

namespace Wiegand26;

public class Sequence
{
    public byte CurrentFacilityCode { get; private set; }

    public ushort CurrentCardNumber { get; private set; }

    public byte FinalFacilityCode { get; private set; }

    public ushort FinalCardNumber { get; private set; }

    public static Sequence NewSequence()
    {
        return new Sequence(byte.MinValue, ushort.MinValue, byte.MaxValue, ushort.MaxValue);
    }

    public static Result<Sequence> From(ulong initialSequence, ulong finalSequence)
    {
        if (!WiegandParser.TryParse(initialSequence, out var initialFacilityCode, out var initialCardNumber)) return Result.Failure<Sequence>(Errors.InvalidLongForWiegand);
        if (!WiegandParser.TryParse(finalSequence, out var finalFacilityCode, out var finalCardNumber)) return Result.Failure<Sequence>(Errors.InvalidLongForWiegand);

        var sequence = new Sequence(initialFacilityCode, initialCardNumber, finalFacilityCode, finalCardNumber);

        return Result.Success(sequence);
    }

    private Sequence(byte initialFacilityCode, ushort initialCardNumber, byte finalFacilityCode, ushort finalCardNumber)
    {
        CurrentFacilityCode = initialFacilityCode;
        CurrentCardNumber = initialCardNumber;
        FinalFacilityCode = finalFacilityCode;
        FinalCardNumber = finalCardNumber;
    }

    public Result<Card> Next()
    {
        byte nextFacilityCode = CurrentFacilityCode;
        ushort nextCardNumber;

        if (!CanIncrement(CurrentCardNumber, ushort.MaxValue))
        {
            if (!CanIncrement(CurrentFacilityCode, FinalFacilityCode))
            {
                return Result.Failure<Card>(Errors.YouRanOutOfCards);
            }
            else
            {
                nextFacilityCode = (byte)Increment(CurrentFacilityCode);
            }

            nextCardNumber = 0;
        }
        else
        {
            nextCardNumber = (ushort)Increment(CurrentCardNumber);
        }

        if (nextFacilityCode > FinalFacilityCode) return Result.Failure<Card>(Errors.YouRanOutOfCards);
        if (nextFacilityCode == FinalFacilityCode && nextCardNumber > FinalCardNumber) return Result.Failure<Card>(Errors.YouRanOutOfCards);

        CurrentFacilityCode = nextFacilityCode;
        CurrentCardNumber = nextCardNumber;

        return Card.From(CurrentFacilityCode, CurrentCardNumber);
    }
    private int Increment(ushort cardNumber)
    {
        return cardNumber + 1;
    }

    private int Increment(byte facilityCode)
    {
        return facilityCode + 1;
    }

    private bool CanIncrement(ushort value, ushort maxValue)
    {
        return value < maxValue;
    }

    private bool CanIncrement(byte value, byte maxValue)
    {
        return value < maxValue;
    }

    public static class Errors
    {
        public const string YouRanOutOfCards = "There's no more cards left";

        public const string InvalidLongForWiegand = "Long value is invalid for wiegand parsing";
    }
}
