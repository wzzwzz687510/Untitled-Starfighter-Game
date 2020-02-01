using System.Collections;
using System.Collections.Generic;

public static class Extensions
{
    public static int GetStableHashCode(this string text)
    {
        unchecked {
            int hash = 23;
            foreach (char c in text)
                hash = hash * 31 + c;
            return hash;
        }
    }

    public static Equipment GetEquipment(this int hash) => Equipment.Dict[hash];
}
