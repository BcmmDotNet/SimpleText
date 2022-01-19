using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;



namespace SimpleText
{
    internal static class DrawingContextExtension
    {
        internal static void DrawSimpleParagraph(this DrawingContext drawingContext, SimpleParagraph paragraph, Point originPoint)
        {
            if (string.IsNullOrEmpty(paragraph.Text))
            {
                return;
            }

            FontFamily fontFamily;
            try
            {
                fontFamily = new FontFamily(paragraph.FontFamily);
            }
            catch
            {
                return;
            }

            Typeface typeface = new Typeface(fontFamily, paragraph.Style, paragraph.Weight, FontStretches.Normal);
            GlyphTypeface? glyphTypeface;
            if (typeface.TryGetGlyphTypeface(out glyphTypeface) is false)
            {
                return;
            }

            if (glyphTypeface is null)
            {
                return;
            }

            var glyphMap = glyphTypeface.CharacterToGlyphMap;
            double y = originPoint.Y;
            double offset = originPoint.X;
            var baseLine = GetBaseline(fontFamily, paragraph.FontSize);

            foreach (var charItem in paragraph.Text)
            {
                if (glyphMap.TryGetValue(charItem, out var glyphIndex) is false)
                {
                    continue;
                }

                var width = glyphTypeface.AdvanceWidths[glyphIndex] * paragraph.FontSize;
                width = RefineValue(width);

                var glyphRun = new GlyphRun(
                    glyphTypeface: glyphTypeface,
                    bidiLevel: 0,
                    isSideways: false,
                    renderingEmSize: paragraph.FontSize,
                    pixelsPerDip: 96,
                    glyphIndices: new[] { glyphIndex },
                    baselineOrigin: new Point(offset, baseLine + y),
                    advanceWidths: new[] { width },
                    glyphOffsets: DefaultGlyphOffsetArray,
                    characters: new char[] { charItem },
                    deviceFontName: null,
                    clusterMap: null,
                    caretStops: null,
                    language: DefaultXmlLanguage);

                drawingContext.DrawGlyphRun(paragraph.Foreground, glyphRun);
                offset += width;
            }
        }

        /// <summary>
        /// 获取指定字体的baseline
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <param name="fontRenderingEmSize"></param>
        /// <returns></returns>
        private static double GetBaseline(FontFamily fontFamily, double fontRenderingEmSize)
        {
            var baseline = fontFamily.Baseline;

            var renderingEmSize = fontRenderingEmSize;

            var value = baseline * renderingEmSize;
            return RefineValue(value);
        }

        /// <summary>
        /// Scale LS ideal resolution value to real value
        /// </summary>
        private static double IdealToRealWithNoRounding(double i)
        {
            return i * DefaultIdealToReal;
        }

        private static double RefineValue(double i)
        {
            var value = IdealToRealWithNoRounding(RealToIdeal(i));

            if (i > 0)
            {
                // Non-zero values should not be converted to 0 accidentally through rounding, ensure that at least the min value is returned.
                value = Math.Max(value, DefaultIdealToReal);
            }

            return value;
        }

        /// <summary>
        /// 从实际值转换为理想值。
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int RealToIdeal(double i)
        {
            int value = (int)Math.Round(i * DefaultRealToIdeal);
            if (i > 0)
            {
                // Non-zero values should not be converted to 0 accidentally through rounding, ensure that at least the min value is returned.
                value = Math.Max(value, 1);
            }

            return value;
        }

        private const double DefaultRealToIdeal = 28800.0 / 96;
        private const double DefaultIdealToReal = 1 / DefaultRealToIdeal;

        private static readonly List<Point> DefaultGlyphOffsetArray = new List<Point>() { new Point() };

        private static readonly XmlLanguage DefaultXmlLanguage =
            XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.IetfLanguageTag);
    }
}
