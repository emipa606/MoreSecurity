using UnityEngine;

namespace SquirtingElephant.Helpers;

public static class Extensions
{
    extension(Rect r)
    {
        public Rect Add_X(float x)
        {
            return new Rect(r.x + x, r.y, r.width, r.height);
        }

        public Rect Replace_Width(float width)
        {
            return new Rect(r.x, r.y, width, r.height);
        }
    }
}