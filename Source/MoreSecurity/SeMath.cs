using System;

namespace SquirtingElephant.Helpers;

public static class SeMath
{
    public static int RoundToNearestMultiple(int value, float multiple)
    {
        return (int)(Math.Round(value / multiple) * multiple);
    }
}