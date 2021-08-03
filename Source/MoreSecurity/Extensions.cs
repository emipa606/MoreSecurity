using UnityEngine;
using Verse;

namespace SquirtingElephant.Helpers
{
    public static class Extensions
    {
        public static Rect Add(this Rect r1, Rect r2)
        {
            return new Rect(r1.x + r2.x, r1.y + r2.y, r1.width + r2.width, r1.height + r2.height);
        }

        public static Rect Subtract(this Rect r1, Rect r2)
        {
            return new Rect(r1.x - r2.x, r1.y - r2.y, r1.width - r2.width, r1.height - r2.height);
        }

        public static Rect Add_X(this Rect r, float x)
        {
            return new Rect(r.x + x, r.y, r.width, r.height);
        }

        public static Rect Add_Y(this Rect r, float y)
        {
            return new Rect(r.x, r.y + y, r.width, r.height);
        }

        public static Rect Add_Width(this Rect r, float width)
        {
            return new Rect(r.x, r.y, r.width + width, r.height);
        }

        public static Rect Add_Height(this Rect r, float height)
        {
            return new Rect(r.x, r.y, r.width, r.height + height);
        }

        public static Rect Replace_X(this Rect r, float x)
        {
            return new Rect(x, r.y, r.width, r.height);
        }

        public static Rect Replace_Y(this Rect r, float y)
        {
            return new Rect(r.x, y, r.width, r.height);
        }

        public static Rect Replace_Width(this Rect r, float width)
        {
            return new Rect(r.x, r.y, width, r.height);
        }

        public static Rect Replace_Height(this Rect r, float height)
        {
            return new Rect(r.x, r.y, r.width, height);
        }

        public static void BeginScrollView(ref Listing_Standard listingStandard, Rect rect, ref Vector2 position,
            ref Rect viewRect)
        {
            Widgets.BeginScrollView(rect, ref position, viewRect);
            rect.height = 100000f;
            rect.width -= 20f;
            listingStandard.Begin(rect.AtZero());
        }

        public static void EndScrollView(ref Listing_Standard listingStandard, ref Rect viewRect, float width,
            float height)
        {
            viewRect = new Rect(0f, 0f, width, height);
            Widgets.EndScrollView();
            listingStandard.End();
        }

        /// <summary>
        ///     Translates and capitalizes the first character.
        /// </summary>
        public static string TC(this string s)
        {
            return s.Translate().CapitalizeFirst();
        }
    }
}