namespace Wiegand26;

public struct Card
{
    public static readonly Card ZeroCode = new Card();

    public byte FacilityCode { get; private set; }

    public ushort CardNumber { get; private set; }

    private Card(byte facilityCode, ushort cardNumber)
    {
        FacilityCode = facilityCode;
        CardNumber = cardNumber;
    }

    public static Card From(byte facilityCode, ushort cardNumber)
    {
        var card = new Card(facilityCode, cardNumber);

        return card;
    }

    public override string ToString()
    {
        return $"{FacilityCode:D3}{CardNumber:D5}";
    }

    public long ToLong()
    {
        return long.Parse(ToString());
    }
}
