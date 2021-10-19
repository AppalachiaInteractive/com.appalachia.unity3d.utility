using Appalachia.Utility.Colors;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Appalachia.Utility.Tests
{
    public class ColorTests
    {
        [SetUp]
        public static void Setup()
        {
            Colors.Colors.ClearHexLookup();
        }

        [Test]
        public void TestColors()
        {
            var aqua1 = Colors.Colors.Aquamarine1;

            Assert.AreApproximatelyEqual(
                aqua1.r,
                127f / 255f,
                "Red channel matches expected value."
            );
            Assert.AreApproximatelyEqual(
                aqua1.g,
                255f / 255f,
                "Green channel matches expected value."
            );
            Assert.AreApproximatelyEqual(
                aqua1.b,
                212f / 255f,
                "Blue channel matches expected value."
            );
            Assert.AreApproximatelyEqual(aqua1.a, 1.0f, "Alpha channel matches expected value.");
        }

        [Test]
        public void TestUpdateRGBA()
        {
            var referenceColor = Color.black;

            referenceColor = referenceColor.UpdateR(0.1f).UpdateG(0.2f).UpdateB(0.3f).UpdateA(0.4f);

            Assert.AreApproximatelyEqual(
                referenceColor.r,
                0.1f,
                "Red channel of color should match explicitly updated value."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.g,
                0.2f,
                "Green channel of color should match explicitly updated value."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.b,
                0.3f,
                "Blue channel of color should match explicitly updated value."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.a,
                0.4f,
                "Alpha channel of color should match explicitly updated value."
            );
        }

        [TestCaseSource(typeof(ColorTestGenerator), nameof(ColorTestGenerator.ColorTestCases))]
        public void TestColorEnumEquality(Colors.Colors.Enum testEnum, Color referenceColor)
        {
            var testValue = Colors.Colors.FromEnum(testEnum);

            Assert.AreApproximatelyEqual(
                referenceColor.r,
                testValue.r,
                $"[{testEnum.ToString()}] Red channel of both colors match."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.g,
                testValue.g,
                $"[{testEnum.ToString()}] Green channel of both colors match."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.b,
                testValue.b,
                $"[{testEnum.ToString()}] Blue channel of both colors match."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.a,
                testValue.a,
                $"[{testEnum.ToString()}] Alpha channel of both colors match."
            );
        }

        [TestCaseSource(typeof(ColorTestGenerator), nameof(ColorTestGenerator.ColorTestCases))]
        public void TestColorBlendingHSV(Colors.Colors.Enum testEnum, Color referenceColor)
        {
            Color.RGBToHSV(referenceColor, out var h, out var s, out var v);

            var black = new Color(0f, 0f, 0f, 0f);
            var blended = referenceColor.BlendHSV(black);
            blended.ToHSV(out var nh, out var ns, out var nv);

            Assert.AreApproximatelyEqual(
                nh,
                h * 0.5f,
                $"[{testEnum.ToString()}] Hue of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                ns,
                s * 0.5f,
                $"[{testEnum.ToString()}] Saturation of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                nv,
                v * 0.5f,
                $"[{testEnum.ToString()}] Value of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                blended.a,
                0.5f,
                $"[{testEnum.ToString()}] Value of alpha blended with invisible should be halved."
            );
        }

        [TestCaseSource(typeof(ColorTestGenerator), nameof(ColorTestGenerator.ColorTestCases))]
        public void TestColorBlendingRGB(Colors.Colors.Enum testEnum, Color referenceColor)
        {
            var black = new Color(0f, 0f, 0f, 0f);
            var blended = referenceColor.BlendRGB(black);

            Assert.AreApproximatelyEqual(
                blended.r,
                referenceColor.r * 0.5f,
                $"[{testEnum.ToString()}] Red channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                blended.g,
                referenceColor.g * 0.5f,
                $"[{testEnum.ToString()}] Green channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                blended.b,
                referenceColor.b * 0.5f,
                $"[{testEnum.ToString()}] Blue channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                blended.a,
                referenceColor.a,
                $"[{testEnum.ToString()}] Alpha channel should still match original.."
            );
        }

        [TestCaseSource(typeof(ColorTestGenerator), nameof(ColorTestGenerator.ColorTestCases))]
        public void TestColorBlendingRGBA(Colors.Colors.Enum testEnum, Color referenceColor)
        {
            var black = new Color(0f, 0f, 0f, 0f);
            var blended = referenceColor.BlendRGBA(black);

            Assert.AreApproximatelyEqual(
                blended.r,
                referenceColor.r * 0.5f,
                $"[{testEnum.ToString()}] Red channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                blended.g,
                referenceColor.g * 0.5f,
                $"[{testEnum.ToString()}] Green channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                blended.b,
                referenceColor.b * 0.5f,
                $"[{testEnum.ToString()}] Blue channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                blended.a,
                referenceColor.a * 0.5f,
                $"[{testEnum.ToString()}] Alpha channel of color blended with invisible should be halved."
            );
        }

        [Test]
        public void TestColorReferenceWrapperUpdateRGBA()
        {
            var referenceColor = Color.black.ToRef();
            referenceColor.UpdateR(0.1f);
            referenceColor.UpdateG(0.2f);
            referenceColor.UpdateB(0.3f);
            referenceColor.UpdateA(0.4f);

            Assert.AreApproximatelyEqual(
                referenceColor.color.r,
                0.1f,
                "Red channel of color should match explicitly updated value."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.color.g,
                0.2f,
                "Green channel of color should match explicitly updated value."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.color.b,
                0.3f,
                "Blue channel of color should match explicitly updated value."
            );
            Assert.AreApproximatelyEqual(
                referenceColor.color.a,
                0.4f,
                "Alpha channel of color should match explicitly updated value."
            );
        }

        [TestCaseSource(typeof(ColorTestGenerator), nameof(ColorTestGenerator.ColorTestCases))]
        public void TestColorReferenceWrapper(Colors.Colors.Enum testEnum, Color referenceColor)
        {
            var wrapper = referenceColor.ToRef();
            wrapper.ScaleR(0.1f);
            wrapper.ScaleG(0.2f);
            wrapper.ScaleB(0.3f);
            wrapper.ScaleA(0.4f);

            Assert.AreApproximatelyEqual(
                wrapper.color.r,
                referenceColor.r * 0.1f,
                $"[{testEnum.ToString()}] Red channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                wrapper.color.g,
                referenceColor.g * 0.2f,
                $"[{testEnum.ToString()}] Green channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                wrapper.color.b,
                referenceColor.b * 0.3f,
                $"[{testEnum.ToString()}] Blue channel of color blended with black should be halved."
            );
            Assert.AreApproximatelyEqual(
                wrapper.color.a,
                referenceColor.a * 0.4f,
                $"[{testEnum.ToString()}] Alpha channel of color blended with invisible should be halved."
            );
        }

        [Test]
        public void TestHexCodeGenerationShort()
        {
            var black = new Color(0f, 0f, 0f, 0f);
            var red = new Color(1f,   0f, 0f, 1f);
            var green = new Color(0f, 1f, 0f, 1f);
            var blue = new Color(0f,  0f, 1f, 1f);
            var white = new Color(1f, 1f, 1f, 1f);

            var blackHex = black.ToHexCodeShort();
            var redHex = red.ToHexCodeShort();
            var greenHex = green.ToHexCodeShort();
            var blueHex = blue.ToHexCodeShort();
            var whiteHex = white.ToHexCodeShort();

            Assert.AreEqual("000000", blackHex, "Black hex code should match.");
            Assert.AreEqual("FF0000", redHex,   "Red hex code should match.");
            Assert.AreEqual("00FF00", greenHex, "Green hex code should match.");
            Assert.AreEqual("0000FF", blueHex,  "Blue hex code should match.");
            Assert.AreEqual("FFFFFF", whiteHex, "White hex code should match.");
        }

        [Test]
        public void TestHexCodeGenerationFull()
        {
            var black = new Color(0f, 0f, 0f, 0f);
            var red = new Color(1f,   0f, 0f, 1f);
            var green = new Color(0f, 1f, 0f, 1f);
            var blue = new Color(0f,  0f, 1f, 1f);
            var white = new Color(1f, 1f, 1f, 1f);

            var blackHex = black.ToHexCodeFull();
            var redHex = red.ToHexCodeFull();
            var greenHex = green.ToHexCodeFull();
            var blueHex = blue.ToHexCodeFull();
            var whiteHex = white.ToHexCodeFull();

            Assert.AreEqual("#00000000", blackHex, "Black hex code should match.");
            Assert.AreEqual("#FFFF0000", redHex,   "Red hex code should match.");
            Assert.AreEqual("#FF00FF00", greenHex, "Green hex code should match.");
            Assert.AreEqual("#FF0000FF", blueHex,  "Blue hex code should match.");
            Assert.AreEqual("#FFFFFFFF", whiteHex, "White hex code should match.");
        }

        [Test]
        public void TestHexCodeParsingShort()
        {
            var black = new Color(0f, 0f, 0f);
            var red = new Color(1f,   0f, 0f);
            var green = new Color(0f, 1f, 0f);
            var blue = new Color(0f,  0f, 1f);
            var white = new Color(1f, 1f, 1f);

            var blackHex = "000000";
            var redHex = "FF0000";
            var greenHex = "00FF00";
            var blueHex = "0000FF";
            var whiteHex = "FFFFFF";

            Assert.AreEqual(
                black,
                blackHex.ColorFromHex(),
                "Black hex code should be parsed correctly."
            );
            Assert.AreEqual(red, redHex.ColorFromHex(), "Red hex code should be parsed correctly.");
            Assert.AreEqual(
                green,
                greenHex.ColorFromHex(),
                "Green hex code should be parsed correctly."
            );
            Assert.AreEqual(
                blue,
                blueHex.ColorFromHex(),
                "Blue hex code should be parsed correctly."
            );
            Assert.AreEqual(
                white,
                whiteHex.ColorFromHex(),
                "White hex code should be parsed correctly."
            );
        }

        [Test]
        public void TestHexCodeParsingShortLower()
        {
            var black = new Color(0f, 0f, 0f);
            var red = new Color(1f,   0f, 0f);
            var green = new Color(0f, 1f, 0f);
            var blue = new Color(0f,  0f, 1f);
            var white = new Color(1f, 1f, 1f);

            var blackHex = "000000";
            var redHex = "ff0000";
            var greenHex = "00ff00";
            var blueHex = "0000ff";
            var whiteHex = "ffffff";

            Assert.AreEqual(
                black,
                blackHex.ColorFromHex(),
                "Black hex code should be parsed correctly."
            );
            Assert.AreEqual(red, redHex.ColorFromHex(), "Red hex code should be parsed correctly.");
            Assert.AreEqual(
                green,
                greenHex.ColorFromHex(),
                "Green hex code should be parsed correctly."
            );
            Assert.AreEqual(
                blue,
                blueHex.ColorFromHex(),
                "Blue hex code should be parsed correctly."
            );
            Assert.AreEqual(
                white,
                whiteHex.ColorFromHex(),
                "White hex code should be parsed correctly."
            );
        }

        [Test]
        public void TestHexCodeParsingFull()
        {
            var black = new Color(0f, 0f, 0f, 0f);
            var red = new Color(1f,   0f, 0f, 1f);
            var green = new Color(0f, 1f, 0f, 1f);
            var blue = new Color(0f,  0f, 1f, 1f);
            var white = new Color(1f, 1f, 1f, 1f);

            var blackHex = "#00000000";
            var redHex = "#FFFF0000";
            var greenHex = "#FF00FF00";
            var blueHex = "#FF0000FF";
            var whiteHex = "#FFFFFFFF";

            Assert.AreEqual(
                black,
                blackHex.ColorFromHex(),
                "Black hex code should be parsed correctly."
            );
            Assert.AreEqual(red, redHex.ColorFromHex(), "Red hex code should be parsed correctly.");
            Assert.AreEqual(
                green,
                greenHex.ColorFromHex(),
                "Green hex code should be parsed correctly."
            );
            Assert.AreEqual(
                blue,
                blueHex.ColorFromHex(),
                "Blue hex code should be parsed correctly."
            );
            Assert.AreEqual(
                white,
                whiteHex.ColorFromHex(),
                "White hex code should be parsed correctly."
            );
        }

        [TestCaseSource(typeof(ColorTestGenerator), nameof(ColorTestGenerator.ColorTestCases))]
        public void TestColorHexReversabilityFull(Colors.Colors.Enum testEnum, Color referenceColor)
        {
            var hexCode = referenceColor.ToHexCodeFull();
            var reverseColor = hexCode.ColorFromHex();

            Assert.AreEqual(
                referenceColor,
                reverseColor,
                $"[{testEnum.ToString()}] The hex code produced for a color should be able to be parsed back to the same color."
            );
        }

        [TestCaseSource(typeof(ColorTestGenerator), nameof(ColorTestGenerator.ColorTestCases))]
        public void TestColorHexReversabilityShort(
            Colors.Colors.Enum testEnum,
            Color referenceColor)
        {
            var hexCode = referenceColor.ToHexCodeShort();
            var reverseColor = hexCode.ColorFromHex();

            Assert.AreEqual(
                referenceColor,
                reverseColor,
                $"[{testEnum.ToString()}] The hex code produced for a color should be able to be parsed back to the same color."
            );
        }
    }
}
