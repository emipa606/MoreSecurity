using UnityEngine;

namespace SquirtingElephant.Helpers;

public static class Extensions
{
    public static Rect Add_X(this Rect r, float x)
    {
        return new Rect(r.x + x, r.y, r.width, r.height);
    }

    public static Rect Replace_Width(this Rect r, float width)
    {
        return new Rect(r.x, r.y, width, r.height);
    }
}