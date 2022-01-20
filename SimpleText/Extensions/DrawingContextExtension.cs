using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace SimpleText
{
    internal static class DrawingContextExtension
    {
        /// <summary>
        /// 使用一个静态词典来缓存已经加载的字体族。
        /// </summary>
        private static readonly  Dictionary<string,FontFamily> FontFamilies = new();

        internal static void DrawSimpleRun(this DrawingContext dc, SimpleRun run)
        {
            if (run.GlyphChars is null || run.GlyphChars.Count == 0)
            {
                return;
            }

            double y = 0;
            double offset = 0;
            double maxRowHeight = 0;

            foreach (var glyphChar in run.GlyphChars)
            {
                FontFamily? fontFamily;

                // 换行直接跳转至下一行咯。
                if (glyphChar.UnicodeChar.Equals('\r'))
                {
                    maxRowHeight = Math.Max(maxRowHeight, glyphChar.FontSize);
                    y += maxRowHeight;
                    offset = 0;
                    continue;
                }

                if (FontFamilies.TryGetValue(glyphChar.FontFamily, out fontFamily) is false)
                {
                    try
                    {
                        fontFamily = new FontFamily(glyphChar.FontFamily);
                        FontFamilies[glyphChar.FontFamily] = fontFamily; // 存储字体至词典呗。
                    }
                    catch
                    {
                        continue;
                    }
                }

                // 创建一个字体。
                Typeface typeface = new(fontFamily, glyphChar.FontStyle, glyphChar.FontWeight, FontStretches.Normal);
                GlyphTypeface? glyphTypeface;
                if (typeface.TryGetGlyphTypeface(out glyphTypeface) is false)
                {
                    continue;
                }

                var glyphMap = glyphTypeface.CharacterToGlyphMap;
                var baseLine = GetBaseline(fontFamily, glyphChar.FontSize);
                if (glyphMap.TryGetValue(glyphChar.UnicodeChar, out var glyphIndex) is false)
                {
                    continue;
                }

                var width = glyphTypeface.AdvanceWidths[glyphIndex] * glyphChar.FontSize;
                width = RefineValue(width);

                var glyphRun = new GlyphRun(
                    glyphTypeface: glyphTypeface,
                    bidiLevel: 0,
                    isSideways: false,
                    renderingEmSize: glyphChar.FontSize,
                    pixelsPerDip: 96,
                    glyphIndices: new[] { glyphIndex },
                    baselineOrigin: new Point(offset, baseLine + y),
                    advanceWidths: new[] { width },
                    glyphOffsets: DefaultGlyphOffsetArray,
                    characters: new char[] { glyphChar.UnicodeChar },
                    deviceFontName: null,
                    clusterMap: null,
                    caretStops: null,
                    language: DefaultXmlLanguage);

                dc.DrawGlyphRun(glyphChar.Foreground, glyphRun);
                offset += width;
            }
        }

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
