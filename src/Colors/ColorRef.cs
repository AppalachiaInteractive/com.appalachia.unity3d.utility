using System;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    /// <summary>
    ///     Reference typed wrapper for <see cref="UnityEngine.Color" />.
    /// </summary>
    [Serializable]
    public class ColorRef
    {
        public Color color;

        public ColorRef(Color c)
        {
            color = c;
        }

        public static implicit operator Color(ColorRef c)
        {
            return c.color;
        }

        public static implicit operator ColorRef(Color c)
        {
            return new(c);
        }

        public void ToHSV(out float h, out float s, out float v)
        {
            color.ToHSV(out h, out s, out v);
        }

        public void BlendHSV(Color other)
        {
            color = color.BlendHSV(other);
        }

        public void BlendRGB(Color other)
        {
            color = color.BlendRGB(other);
        }

        public void BlendRGBA(Color other)
        {
            color = color.BlendRGBA(other);
        }

        public void BlendHSV(ColorRef other)
        {
            color = color.BlendHSV(other);
        }

        public void BlendRGB(ColorRef other)
        {
            color = color.BlendRGB(other);
        }

        public void BlendRGBA(ColorRef other)
        {
            color = color.BlendRGBA(other);
        }

        public void UpdateH(float val, bool allowHdr = true)
        {
            color = color.UpdateH(val, allowHdr);
        }

        public void UpdateS(float val, bool allowHdr = true)
        {
            color = color.UpdateS(val, allowHdr);
        }

        public void UpdateV(float val, bool allowHdr = true)
        {
            color = color.UpdateV(val, allowHdr);
        }

        public void UpdateR(float val)
        {
            color = color.UpdateR(val);
        }

        public void UpdateG(float val)
        {
            color = color.UpdateG(val);
        }

        public void UpdateB(float val)
        {
            color = color.UpdateB(val);
        }

        public void UpdateA(float val)
        {
            color = color.UpdateA(val);
        }

        public void ScaleH(float factor, bool allowHdr = true)
        {
            color = color.ScaleH(factor, allowHdr);
        }

        public void ScaleS(float factor, bool allowHdr = true)
        {
            color = color.ScaleS(factor, allowHdr);
        }

        public void ScaleV(float factor, bool allowHdr = true)
        {
            color = color.ScaleV(factor, allowHdr);
        }

        public void ScaleR(float factor)
        {
            color = color.ScaleR(factor);
        }

        public void ScaleB(float factor)
        {
            color = color.ScaleB(factor);
        }

        public void ScaleG(float factor)
        {
            color = color.ScaleG(factor);
        }

        public void ScaleA(float factor)
        {
            color = color.ScaleA(factor);
        }

        public void UpdateHue(float newHue, bool allowHdr = true)
        {
            color = color.UpdateHue(newHue, allowHdr);
        }

        public void UpdateSaturation(float newSaturation, bool allowHdr = true)
        {
            color = color.UpdateSaturation(newSaturation, allowHdr);
        }

        public void UpdateValue(float newValue, bool allowHdr = true)
        {
            color = color.UpdateValue(newValue, allowHdr);
        }

        public void UpdateBlue(float val)
        {
            color = color.UpdateBlue(val);
        }

        public void UpdateRed(float val)
        {
            color = color.UpdateRed(val);
        }

        public void UpdateGreen(float val)
        {
            color = color.UpdateGreen(val);
        }

        public void UpdateAlpha(float val)
        {
            color = color.UpdateAlpha(val);
        }

        public void ScaleHue(float factor, bool allowHdr = true)
        {
            color = color.ScaleHue(factor, allowHdr);
        }

        public void ScaleSaturation(float factor, bool allowHdr = true)
        {
            color = color.ScaleSaturation(factor, allowHdr);
        }

        public void ScaleValue(float factor, bool allowHdr = true)
        {
            color = color.ScaleValue(factor, allowHdr);
        }

        public void ScaleRed(float factor)
        {
            color = color.ScaleRed(factor);
        }

        public void ScaleGreen(float factor)
        {
            color = color.ScaleGreen(factor);
        }

        public void ScaleBlue(float factor)
        {
            color = color.ScaleBlue(factor);
        }

        public void ScaleAlpha(float factor)
        {
            color = color.ScaleAlpha(factor);
        }
    }
}
