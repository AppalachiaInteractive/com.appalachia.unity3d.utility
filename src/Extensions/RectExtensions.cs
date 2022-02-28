using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class RectExtensions
    {
        public static Rect AlignBottom(this Rect rect, float height)
        {
            using (_PRF_AlignBottom.Auto())
            {
                rect.y = (rect.y + rect.height) - height;
                rect.height = height;
                return rect;
            }
        }

        public static Rect AlignCenter(this Rect rect, float width)
        {
            using (_PRF_AlignCenter.Auto())
            {
                rect.x = (float)((rect.x + (rect.width * 0.5)) - (width * 0.5));
                rect.width = width;
                return rect;
            }
        }

        public static Rect AlignCenter(this Rect rect, float width, float height)
        {
            using (_PRF_AlignCenter.Auto())
            {
                rect.x = (float)((rect.x + (rect.width * 0.5)) - (width * 0.5));
                rect.y = (float)((rect.y + (rect.height * 0.5)) - (height * 0.5));
                rect.width = width;
                rect.height = height;
                return rect;
            }
        }

        public static Rect AlignCenterX(this Rect rect, float width)
        {
            using (_PRF_AlignCenterX.Auto())
            {
                rect.x = (float)((rect.x + (rect.width * 0.5)) - (width * 0.5));
                rect.width = width;
                return rect;
            }
        }

        public static Rect AlignCenterXY(this Rect rect, float size)
        {
            using (_PRF_AlignCenterXY.Auto())
            {
                rect.y = (float)((rect.y + (rect.height * 0.5)) - (size * 0.5));
                rect.x = (float)((rect.x + (rect.width * 0.5)) - (size * 0.5));
                rect.height = size;
                rect.width = size;
                return rect;
            }
        }

        public static Rect AlignCenterXY(this Rect rect, float width, float height)
        {
            using (_PRF_AlignCenterXY.Auto())
            {
                rect.y = (float)((rect.y + (rect.height * 0.5)) - (height * 0.5));
                rect.x = (float)((rect.x + (rect.width * 0.5)) - (width * 0.5));
                rect.width = width;
                rect.height = height;
                return rect;
            }
        }

        public static Rect AlignCenterY(this Rect rect, float height)
        {
            using (_PRF_AlignCenterY.Auto())
            {
                rect.y = (float)((rect.y + (rect.height * 0.5)) - (height * 0.5));
                rect.height = height;
                return rect;
            }
        }

        public static Rect AlignLeft(this Rect rect, float width)
        {
            using (_PRF_AlignLeft.Auto())
            {
                rect.width = width;
                return rect;
            }
        }

        public static Rect AlignMiddle(this Rect rect, float height)
        {
            using (_PRF_AlignMiddle.Auto())
            {
                rect.y = (float)((rect.y + (rect.height * 0.5)) - (height * 0.5));
                rect.height = height;
                return rect;
            }
        }

        public static Rect AlignRight(this Rect rect, float width)
        {
            using (_PRF_AlignRight.Auto())
            {
                rect.x = (rect.x + rect.width) - width;
                rect.width = width;
                return rect;
            }
        }

        public static Rect AlignRight(this Rect rect, float width, bool clamp)
        {
            using (_PRF_AlignRight.Auto())
            {
                if (clamp)
                {
                    rect.xMin = Mathf.Max(rect.xMax - width, rect.xMin);
                    return rect;
                }

                rect.x = (rect.x + rect.width) - width;
                rect.width = width;
                return rect;
            }
        }

        public static Rect AlignTop(this Rect rect, float height)
        {
            using (_PRF_AlignTop.Auto())
            {
                rect.height = height;
                return rect;
            }
        }

        public static Rect Encompass(this Rect rect, Vector2 pos)
        {
            using (_PRF_Encompass.Auto())
            {
                if (pos.x < (double)rect.xMin)
                {
                    rect.xMin = pos.x;
                }
                else if (pos.x > (double)rect.xMax)
                {
                    rect.xMax = pos.x;
                }

                if (pos.y < (double)rect.yMin)
                {
                    rect.yMin = pos.y;
                }
                else if (pos.y > (double)rect.yMax)
                {
                    rect.yMax = pos.y;
                }

                return rect;
            }
        }

        public static Rect EnsureHeightIsAtLeast(this Rect rect, float minHeight)
        {
            using (_PRF_EnsureHeightIsAtLeast.Auto())
            {
                rect.height = Mathf.Max(rect.height, minHeight);
                return rect;
            }
        }

        public static Rect EnsureHeightIsAtMost(this Rect rect, float maxHeight)
        {
            using (_PRF_EnsureHeightIsAtMost.Auto())
            {
                rect.height = Mathf.Min(rect.height, maxHeight);
                return rect;
            }
        }

        public static Rect EnsureWidthIsAtLeast(this Rect rect, float minWidth)
        {
            using (_PRF_EnsureWidthIsAtLeast.Auto())
            {
                rect.width = Mathf.Max(rect.width, minWidth);
                return rect;
            }
        }

        public static Rect EnsureWidthIsAtMost(this Rect rect, float maxWidth)
        {
            using (_PRF_EnsureWidthIsAtMost.Auto())
            {
                rect.width = Mathf.Min(rect.width, maxWidth);
                return rect;
            }
        }

        public static Rect Expand(this Rect rect, float expand)
        {
            using (_PRF_Expand.Auto())
            {
                rect.x -= expand;
                rect.y -= expand;
                rect.height += expand * 2f;
                rect.width += expand * 2f;
                return rect;
            }
        }

        public static Rect Expand(this Rect rect, float horizontal, float vertical)
        {
            using (_PRF_Expand.Auto())
            {
                rect.position -= new Vector2(horizontal, vertical);
                rect.size += new Vector2(horizontal, vertical) * 2f;
                return rect;
            }
        }

        public static Rect Expand(this Rect rect, float left, float right, float top, float bottom)
        {
            using (_PRF_Expand.Auto())
            {
                rect.position -= new Vector2(left,     top);
                rect.size += new Vector2(left + right, top + bottom);
                return rect;
            }
        }

        public static Vector2 GetBottomCenter(this Rect rect)
        {
            using (_PRF_GetBottomCenter.Auto())
            {
                return new Vector2(rect.center.x, rect.yMin);
            }
        }

        public static Vector2 GetBottomLeftCorner(this Rect rect)
        {
            using (_PRF_GetBottomLeftCorner.Auto())
            {
                return new Vector2(rect.xMin, rect.yMin);
            }
        }

        public static Vector2 GetBottomRightCorner(this Rect rect)
        {
            using (_PRF_GetBottomRightCorner.Auto())
            {
                return new Vector2(rect.xMax, rect.yMin);
            }
        }

        public static Vector2 GetMiddleLeft(this Rect rect)
        {
            using (_PRF_GetMiddleLeft.Auto())
            {
                return new Vector2(rect.xMin, rect.center.y);
            }
        }

        public static Vector2 GetMiddleRight(this Rect rect)
        {
            using (_PRF_GetMiddleRight.Auto())
            {
                return new Vector2(rect.xMax, rect.center.y);
            }
        }

        public static Vector2 GetNormalizedPositionWithin(
            this Rect rect,
            Vector2 position,
            bool clamped = true)
        {
            using (_PRF_GetNormalizedPositionWithin.Auto())
            {
                var width = (position.x - rect.min.x) / rect.width;
                var height = (position.y - rect.min.y) / rect.height;

                if (clamped)
                {
                    width = Mathf.Clamp01(width);
                    height = Mathf.Clamp01(height);
                }

                var result = new Vector2(width, height);

                return result;
            }
        }

        public static Vector2 GetTopCenter(this Rect rect)
        {
            using (_PRF_TopCenter.Auto())
            {
                return new Vector2(rect.center.x, rect.yMax);
            }
        }

        public static Vector2 GetTopLeft(this Rect rect)
        {
            using (_PRF_TopLeft.Auto())
            {
                return new Vector2(rect.xMin, rect.yMax);
            }
        }

        public static Vector2 GetTopRight(this Rect rect)
        {
            using (_PRF_TopRight.Auto())
            {
                return new Vector2(rect.xMax, rect.yMax);
            }
        }

        public static Rect HorizontalPadding(this Rect rect, float padding)
        {
            using (_PRF_HorizontalPadding.Auto())
            {
                rect.x += padding;
                rect.width -= padding * 2f;
                return rect;
            }
        }

        public static Rect HorizontalPadding(this Rect rect, float left, float right)
        {
            using (_PRF_HorizontalPadding.Auto())
            {
                rect.x += left;
                rect.width -= left + right;
                return rect;
            }
        }

        public static Rect Padding(this Rect rect, float padding)
        {
            using (_PRF_Padding.Auto())
            {
                rect.position += new Vector2(padding, padding);
                rect.size -= new Vector2(padding, padding) * 2f;
                return rect;
            }
        }

        public static Rect Padding(this Rect rect, float horizontal, float vertical)
        {
            using (_PRF_Padding.Auto())
            {
                rect.position += new Vector2(horizontal, vertical);
                rect.size -= new Vector2(horizontal, vertical) * 2f;
                return rect;
            }
        }

        public static Rect Padding(this Rect rect, float left, float right, float top, float bottom)
        {
            using (_PRF_Padding.Auto())
            {
                rect.position += new Vector2(left,     top);
                rect.size -= new Vector2(left + right, top + bottom);
                return rect;
            }
        }

        public static Rect ResetPosition(this Rect rect)
        {
            using (_PRF_ResetPosition.Auto())
            {
                rect.position = Vector2.zero;
                return rect;
            }
        }

        public static Rect SetAverageHorizontalPosition(this Rect rect, float x)
        {
            using (_PRF_SetAverageHorizontalPosition.Auto())
            {
                rect.center = new Vector2(x, rect.center.y);
                return rect;
            }
        }

        public static Rect SetAveragePosition(this Rect rect, float x, float y)
        {
            using (_PRF_SetAveragePosition.Auto())
            {
                rect.center = new Vector2(x, y);
                return rect;
            }
        }

        public static Rect SetAveragePosition(this Rect rect, Vector2 center)
        {
            using (_PRF_SetAveragePosition.Auto())
            {
                rect.center = center;
                return rect;
            }
        }

        public static Rect SetAverageVerticalPosition(this Rect rect, float y)
        {
            using (_PRF_SetAverageVerticalPosition.Auto())
            {
                rect.center = new Vector2(rect.center.x, y);
                return rect;
            }
        }

        public static Rect SetBottomEdge(this Rect rect, float yMin)
        {
            using (_PRF_SetBottomEdge.Auto())
            {
                rect.yMin = yMin;
                return rect;
            }
        }

        public static Rect SetBottomLeftCorner(this Rect rect, Vector2 min)
        {
            using (_PRF_SetBottomLeftCorner.Auto())
            {
                rect.min = min;
                return rect;
            }
        }

        public static Rect SetHeight(this Rect rect, float height)
        {
            using (_PRF_SetHeight.Auto())
            {
                rect.height = height;
                return rect;
            }
        }

        public static Rect SetHorizontalPosition(this Rect rect, float x)
        {
            using (_PRF_SetHorizontalPosition.Auto())
            {
                rect.x = x;
                return rect;
            }
        }

        public static Rect SetLeftEdge(this Rect rect, float xMin)
        {
            using (_PRF_SetLeftEdge.Auto())
            {
                rect.xMin = xMin;
                return rect;
            }
        }

        public static Rect SetPosition(this Rect rect, Vector2 position)
        {
            using (_PRF_SetPosition.Auto())
            {
                rect.position = position;
                return rect;
            }
        }

        public static Rect SetRightEdge(this Rect rect, float xMax)
        {
            using (_PRF_SetRightEdge.Auto())
            {
                rect.xMax = xMax;
                return rect;
            }
        }

        public static Rect SetSize(this Rect rect, float width, float height)
        {
            using (_PRF_SetSize.Auto())
            {
                rect.width = width;
                rect.height = height;
                return rect;
            }
        }

        public static Rect SetSize(this Rect rect, Vector2 size)
        {
            using (_PRF_SetSize.Auto())
            {
                rect.size = size;
                return rect;
            }
        }

        public static Rect SetTopEdge(this Rect rect, float yMax)
        {
            using (_PRF_SetTopEdge.Auto())
            {
                rect.yMax = yMax;
                return rect;
            }
        }

        public static Rect SetTopRightCorner(this Rect rect, Vector2 max)
        {
            using (_PRF_SetTopRightCorner.Auto())
            {
                rect.max = max;
                return rect;
            }
        }

        public static Rect SetVerticalPosition(this Rect rect, float y)
        {
            using (_PRF_SetVerticalPosition.Auto())
            {
                rect.y = y;
                return rect;
            }
        }

        public static Rect SetWidth(this Rect rect, float width)
        {
            using (_PRF_SetWidth.Auto())
            {
                rect.width = width;
                return rect;
            }
        }

        public static Rect Shift(this Rect rect, Vector2 move)
        {
            using (_PRF_Shift.Auto())
            {
                rect.position += move;
                return rect;
            }
        }

        public static Rect Shift(this Rect rect, float x, float y)
        {
            using (_PRF_Shift.Auto())
            {
                rect.x += x;
                rect.y += y;
                return rect;
            }
        }

        public static Rect ShiftBottomEdge(this Rect rect, float value)
        {
            using (_PRF_ShiftBottomEdge.Auto())
            {
                rect.yMin += value;
                return rect;
            }
        }

        public static Rect ShiftBottomRightCorner(this Rect rect, Vector2 value)
        {
            using (_PRF_ShiftBottomRightCorner.Auto())
            {
                rect.min += value;
                return rect;
            }
        }

        public static Rect ShiftHorizontal(this Rect rect, float x)
        {
            using (_PRF_ShiftHorizontal.Auto())
            {
                rect.x += x;
                return rect;
            }
        }

        public static Rect ShiftLeftEdge(this Rect rect, float value)
        {
            using (_PRF_ShiftLeftEdge.Auto())
            {
                rect.xMin += value;
                return rect;
            }
        }

        public static Rect ShiftRightEdge(this Rect rect, float value)
        {
            using (_PRF_ShiftRightEdge.Auto())
            {
                rect.xMax += value;
                return rect;
            }
        }

        public static Rect ShiftTopEdge(this Rect rect, float value)
        {
            using (_PRF_ShiftTopEdge.Auto())
            {
                rect.yMax += value;
                return rect;
            }
        }

        public static Rect ShiftTopRightCorner(this Rect rect, Vector2 value)
        {
            using (_PRF_ShiftTopRightCorner.Auto())
            {
                rect.max += value;
                return rect;
            }
        }

        public static Rect ShiftVertical(this Rect rect, float y)
        {
            using (_PRF_ShiftVertical.Auto())
            {
                rect.y += y;
                return rect;
            }
        }

        public static Rect Split(this Rect rect, int index, int count)
        {
            using (_PRF_Split.Auto())
            {
                var num = rect.width / count;
                rect.width = num;
                rect.x += num * index;
                return rect;
            }
        }

        public static Rect SplitGrid(this Rect rect, float width, float height, int index)
        {
            using (_PRF_SplitGrid.Auto())
            {
                var num1 = (int)(rect.width / (double)width);
                var num2 = num1 > 0 ? num1 : 1;
                var num3 = index % num2;
                var num4 = index / num2;
                rect.x += num3 * width;
                rect.y += num4 * height;
                rect.width = width;
                rect.height = height;
                return rect;
            }
        }

        public static Rect SplitTableGrid(this Rect rect, int columnCount, float rowHeight, int index)
        {
            using (_PRF_SplitTableGrid.Auto())
            {
                var num1 = index % columnCount;
                var num2 = index / columnCount;
                var num3 = rect.width / columnCount;
                rect.x += num1 * num3;
                rect.y += num2 * rowHeight;
                rect.width = num3;
                rect.height = rowHeight;
                return rect;
            }
        }

        public static Rect SplitVertical(this Rect rect, int index, int count)
        {
            using (_PRF_SplitVertical.Auto())
            {
                var num = rect.height / count;
                rect.height = num;
                rect.y += num * index;
                return rect;
            }
        }

        public static Rect VerticalPadding(this Rect rect, float padding)
        {
            using (_PRF_VerticalPadding.Auto())
            {
                rect.y += padding;
                rect.height -= padding * 2f;
                return rect;
            }
        }

        public static Rect VerticalPadding(this Rect rect, float top, float bottom)
        {
            using (_PRF_VerticalPadding.Auto())
            {
                rect.y += top;
                rect.height -= top + bottom;
                return rect;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(RectExtensions) + ".";

        private static readonly ProfilerMarker _PRF_GetBottomCenter =
            new ProfilerMarker(_PRF_PFX + nameof(GetBottomCenter));

        private static readonly ProfilerMarker _PRF_GetMiddleLeft =
            new ProfilerMarker(_PRF_PFX + nameof(GetMiddleLeft));

        private static readonly ProfilerMarker _PRF_GetMiddleRight =
            new ProfilerMarker(_PRF_PFX + nameof(GetMiddleRight));

        private static readonly ProfilerMarker _PRF_EnsureWidthIsAtLeast =
            new ProfilerMarker(_PRF_PFX + nameof(EnsureWidthIsAtLeast));

        private static readonly ProfilerMarker _PRF_EnsureWidthIsAtMost =
            new ProfilerMarker(_PRF_PFX + nameof(EnsureWidthIsAtMost));

        private static readonly ProfilerMarker _PRF_Expand = new ProfilerMarker(_PRF_PFX + nameof(Expand));

        private static readonly ProfilerMarker _PRF_Encompass =
            new ProfilerMarker(_PRF_PFX + nameof(Encompass));

        private static readonly ProfilerMarker _PRF_GetBottomLeftCorner =
            new ProfilerMarker(_PRF_PFX + nameof(GetBottomLeftCorner));

        private static readonly ProfilerMarker _PRF_GetBottomRightCorner =
            new ProfilerMarker(_PRF_PFX + nameof(GetBottomRightCorner));

        private static readonly ProfilerMarker _PRF_TopCenter =
            new ProfilerMarker(_PRF_PFX + nameof(GetTopCenter));

        private static readonly ProfilerMarker _PRF_TopRight =
            new ProfilerMarker(_PRF_PFX + nameof(GetTopRight));

        private static readonly ProfilerMarker _PRF_TopLeft =
            new ProfilerMarker(_PRF_PFX + nameof(GetTopLeft));

        private static readonly ProfilerMarker _PRF_BottomRight =
            new ProfilerMarker(_PRF_PFX + nameof(GetBottomRightCorner));

        private static readonly ProfilerMarker _PRF_BottomLeft =
            new ProfilerMarker(_PRF_PFX + nameof(GetBottomLeftCorner));

        private static readonly ProfilerMarker _PRF_GetNormalizedPositionWithin =
            new ProfilerMarker(_PRF_PFX + nameof(GetNormalizedPositionWithin));

        private static readonly ProfilerMarker _PRF_AlignBottom =
            new ProfilerMarker(_PRF_PFX + nameof(AlignBottom));

        private static readonly ProfilerMarker _PRF_AlignCenter =
            new ProfilerMarker(_PRF_PFX + nameof(AlignCenter));

        private static readonly ProfilerMarker _PRF_AlignCenterX =
            new ProfilerMarker(_PRF_PFX + nameof(AlignCenterX));

        private static readonly ProfilerMarker _PRF_AlignCenterXY =
            new ProfilerMarker(_PRF_PFX + nameof(AlignCenterXY));

        private static readonly ProfilerMarker _PRF_AlignCenterY =
            new ProfilerMarker(_PRF_PFX + nameof(AlignCenterY));

        private static readonly ProfilerMarker _PRF_AlignLeft =
            new ProfilerMarker(_PRF_PFX + nameof(AlignLeft));

        private static readonly ProfilerMarker _PRF_AlignMiddle =
            new ProfilerMarker(_PRF_PFX + nameof(AlignMiddle));

        private static readonly ProfilerMarker _PRF_AlignRight =
            new ProfilerMarker(_PRF_PFX + nameof(AlignRight));

        private static readonly ProfilerMarker
            _PRF_AlignTop = new ProfilerMarker(_PRF_PFX + nameof(AlignTop));

        private static readonly ProfilerMarker _PRF_EnsureHeightIsAtLeast =
            new ProfilerMarker(_PRF_PFX + nameof(EnsureHeightIsAtLeast));

        private static readonly ProfilerMarker _PRF_EnsureHeightIsAtMost =
            new ProfilerMarker(_PRF_PFX + nameof(EnsureHeightIsAtMost));

        private static readonly ProfilerMarker _PRF_GetTopCenter =
            new ProfilerMarker(_PRF_PFX + nameof(GetTopCenter));

        private static readonly ProfilerMarker _PRF_GetTopLeft =
            new ProfilerMarker(_PRF_PFX + nameof(GetTopLeft));

        private static readonly ProfilerMarker _PRF_GetTopRight =
            new ProfilerMarker(_PRF_PFX + nameof(GetTopRight));

        private static readonly ProfilerMarker _PRF_HorizontalPadding =
            new ProfilerMarker(_PRF_PFX + nameof(HorizontalPadding));

        private static readonly ProfilerMarker _PRF_Padding = new ProfilerMarker(_PRF_PFX + nameof(Padding));

        private static readonly ProfilerMarker _PRF_ResetPosition =
            new ProfilerMarker(_PRF_PFX + nameof(ResetPosition));

        private static readonly ProfilerMarker _PRF_SetAverageHorizontalPosition =
            new ProfilerMarker(_PRF_PFX + nameof(SetAverageHorizontalPosition));

        private static readonly ProfilerMarker _PRF_SetAveragePosition =
            new ProfilerMarker(_PRF_PFX + nameof(SetAveragePosition));

        private static readonly ProfilerMarker _PRF_SetAverageVerticalPosition =
            new ProfilerMarker(_PRF_PFX + nameof(SetAverageVerticalPosition));

        private static readonly ProfilerMarker _PRF_SetBottomEdge =
            new ProfilerMarker(_PRF_PFX + nameof(SetBottomEdge));

        private static readonly ProfilerMarker _PRF_SetBottomLeftCorner =
            new ProfilerMarker(_PRF_PFX + nameof(SetBottomLeftCorner));

        private static readonly ProfilerMarker _PRF_SetHeight =
            new ProfilerMarker(_PRF_PFX + nameof(SetHeight));

        private static readonly ProfilerMarker _PRF_SetHorizontalPosition =
            new ProfilerMarker(_PRF_PFX + nameof(SetHorizontalPosition));

        private static readonly ProfilerMarker _PRF_SetLeftEdge =
            new ProfilerMarker(_PRF_PFX + nameof(SetLeftEdge));

        private static readonly ProfilerMarker _PRF_SetPosition =
            new ProfilerMarker(_PRF_PFX + nameof(SetPosition));

        private static readonly ProfilerMarker _PRF_SetRightEdge =
            new ProfilerMarker(_PRF_PFX + nameof(SetRightEdge));

        private static readonly ProfilerMarker _PRF_SetSize = new ProfilerMarker(_PRF_PFX + nameof(SetSize));

        private static readonly ProfilerMarker _PRF_SetTopEdge =
            new ProfilerMarker(_PRF_PFX + nameof(SetTopEdge));

        private static readonly ProfilerMarker _PRF_SetTopRightCorner =
            new ProfilerMarker(_PRF_PFX + nameof(SetTopRightCorner));

        private static readonly ProfilerMarker _PRF_SetVerticalPosition =
            new ProfilerMarker(_PRF_PFX + nameof(SetVerticalPosition));

        private static readonly ProfilerMarker
            _PRF_SetWidth = new ProfilerMarker(_PRF_PFX + nameof(SetWidth));

        private static readonly ProfilerMarker _PRF_Shift = new ProfilerMarker(_PRF_PFX + nameof(Shift));

        private static readonly ProfilerMarker _PRF_ShiftBottomEdge =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftBottomEdge));

        private static readonly ProfilerMarker _PRF_ShiftBottomRightCorner =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftBottomRightCorner));

        private static readonly ProfilerMarker _PRF_ShiftHorizontal =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftHorizontal));

        private static readonly ProfilerMarker _PRF_ShiftLeftEdge =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftLeftEdge));

        private static readonly ProfilerMarker _PRF_ShiftRightEdge =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftRightEdge));

        private static readonly ProfilerMarker _PRF_ShiftTopEdge =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftTopEdge));

        private static readonly ProfilerMarker _PRF_ShiftTopRightCorner =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftTopRightCorner));

        private static readonly ProfilerMarker _PRF_ShiftVertical =
            new ProfilerMarker(_PRF_PFX + nameof(ShiftVertical));

        private static readonly ProfilerMarker _PRF_Split = new ProfilerMarker(_PRF_PFX + nameof(Split));

        private static readonly ProfilerMarker _PRF_SplitGrid =
            new ProfilerMarker(_PRF_PFX + nameof(SplitGrid));

        private static readonly ProfilerMarker _PRF_SplitTableGrid =
            new ProfilerMarker(_PRF_PFX + nameof(SplitTableGrid));

        private static readonly ProfilerMarker _PRF_SplitVertical =
            new ProfilerMarker(_PRF_PFX + nameof(SplitVertical));

        private static readonly ProfilerMarker _PRF_VerticalPadding =
            new ProfilerMarker(_PRF_PFX + nameof(VerticalPadding));

        #endregion
    }
}
