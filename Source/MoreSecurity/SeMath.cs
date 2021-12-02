using System;

namespace SquirtingElephant.Helpers;

public static class SeMath
{
    public static int RoundToNearestMultiple(int value, float multiple)
    {
        return (int)(Math.Round(value / multiple) * multiple);
    }

    /// <summary>
    ///     Calculates the X-location of the column on <c>colIdx</c>.
    /// </summary>
    /// <param name="offset_X">X-offset for the first column.</param>
    /// <param name="colWidth">Column Width (each column has the same width).</param>
    /// <param name="spacing_X">Spacing between columns.</param>
    /// <param name="colIdx">Column index for the column that you need the x-location for.</param>
    /// <returns></returns>
    public static float CalcColumn_X(float offset_X, float colWidth, float spacing_X, int colIdx)
    {
        return offset_X + ((colWidth + spacing_X) * colIdx);
    }
}