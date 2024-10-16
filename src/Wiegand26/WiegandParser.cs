using System;
using System.Collections;

namespace Wiegand26;

public static class WiegandParser
{
    public static bool TryParse(ulong value, out byte facilityCode, out ushort cardNumber)
    {
        facilityCode = byte.MinValue;
        cardNumber = ushort.MinValue;

        // Doing string manipulation since here I'm considering that the long value doesn't actually represent the exact 26 bits of the Wiegand card format.
        // At a low-level scenario, it would be best to use BitArray to represent the wiegand data.

        var strValue = value.ToString().PadLeft(8, '0');
        if (strValue.Length > 8) return false;

        if (!byte.TryParse(strValue.Substring(0, 3), out facilityCode)) return false;
        if (!ushort.TryParse(strValue.Substring(3, 5), out cardNumber)) return false;

        return true;
    }
}
