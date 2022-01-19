using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace GlyphsTest
{
    public class CustomTextEditor : FrameworkElement
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomTextEditor), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public int FontSize
        {
            get => (int)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(int), typeof(CustomTextEditor), new FrameworkPropertyMetadata(12, FrameworkPropertyMetadataOptions.AffectsRender));

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                return;
            }

            var fontFamily = new FontFamily("微软雅黑");
            var typeface = fontFamily.GetTypefaces().Skip(1).Take(1).FirstOrDefault();
            GlyphTypeface? glyphTypeface = null;

            if (typeface?.TryGetGlyphTypeface(out glyphTypeface) is false)
            {
                return;
            }

            if (glyphTypeface is null)
            {
                return;
            }

            var glyphMap = glyphTypeface.CharacterToGlyphMap;
            double y = 0;
            double offset = 3;
            var baseLine = GetBaseline(fontFamily, this.FontSize);

            MatrixTransform matrixTransform = new MatrixTransform(m11: 1, m12: 1, m21: -1, m22: 1, offsetX: 0, offsetY: 0);
            //drawingContext.PushTransform(matrixTransform);

            foreach (var charItem in this.Text)
            {
                if (glyphMap.TryGetValue(charItem, out var glyphIndex) is false)
                {
                    continue;
                }

                var width = glyphTypeface.AdvanceWidths[glyphIndex] * this.FontSize;
                width = RefineValue(width);

                var glyphRun = new GlyphRun(
                    glyphTypeface: glyphTypeface,
                    bidiLevel: 0,
                    isSideways: false,
                    renderingEmSize: this.FontSize,
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

                drawingContext.DrawGlyphRun(Brushes.Red, glyphRun);
                offset += width;
            }

            //drawingContext.Pop();
        }

        /// <summary>
        /// Constructs a new GlyphRun object for per monitor DPI aware applications
        /// 为每监视器DPI感知应用程序构造新的GlyphRun对象
        /// </summary>
        /// <param name="glyphTypeface">GlyphTypeface of the GlyphRun object </param>
        /// <param name="bidiLevel">Bidi level of the GlyphRun object
        /// 指定双向布局级别。 偶数和零值表示从左到右布局；奇数值表示从右到左布局。</param>
        /// <param name="isSideways">Set to true to display the GlyphRun sideways
        /// 设置文本是否倾斜</param>
        /// <param name="renderingEmSize">Font rendering size in drawing surface units (96ths of an inch).
        /// 以图形表面单位（96分之一英寸）表示的字体渲染大小。</param>
        /// <param name="pixelsPerDip">PixelsPerDip of the screen on which this is to be drawn (96ths of an inch).
        /// 要绘制的屏幕的像素（96分之一英寸）。</param>
        /// <param name="glyphIndices">The list of font indices that represent glyphs in this run.
        /// 表示Run中字形的字体索引列表。</param>
        /// <param name="baselineOrigin">Origin of the first glyph in the run.
        /// The glyph is placed so that the leading edge of its advance vector
        /// and its baseline intersect this point.
        /// 放置图示符时，其前进矢量的前缘
        /// 它的基线与这一点相交。
        ///  </param>
        /// <param name="advanceWidths">The list of advance widths, one for each glyph in GlyphIndices.
        /// The nominal origin of the nth glyph (n > 0) in the run is the nominal origin
        /// of the n-1th glyph plus the n-1th advance width added along the runs advance vector.
        /// Base glyphs generally have a non-zero advance width, combining glyphs generally have a zero advance width.
        /// </param>
        /// <param name="glyphOffsets">The list of glyph offsets. Added to the nominal glyph origin calculated above to generate the final origin for the glyph.
        /// Base glyphs generally have a glyph offset of (0,0), combining glyphs generally have an offset
        /// that places them correctly on top of the nearest preceeding base glyph.
        /// </param>
        /// <param name="characters">Characters represented by this glyphrun</param>
        /// <param name="deviceFontName">
        /// Identifies a specific device font for which the GlyphRun has been optimized. When a GlyphRun is
        /// being rendered on a device that has built-in support for this named font, then the GlyphRun should be rendered using a
        /// possibly device specific mechanism for selecting that font, and by sending the Unicode codepoints rather than the
        /// glyph indices. When rendering onto a device that does not include built-in support for the named font,
        /// this property should be ignored.
        /// </param>
        /// <param name="clusterMap">The list that maps characters in the glyph run to glyph indices.
        /// There is one entry per character in Characters list.
        /// Each value gives the offset of the first glyph in GlyphIndices
        /// that represents the corresponding character in Characters.
        /// Where multiple characters map to a single glyph, or to a glyph group
        /// that cannot be broken down to map exactly to individual characters,
        /// the entries for all the characters have the same value:
        /// the offset of the first glyph that represents this group of characters.
        /// If the list is null or empty, sequential 1 to 1 mapping is assumed.
        /// </param>
        /// <param name="caretStops">A list of caret stops for the glyphs</param>
        /// <param name="language">Language of the GlyphRun</param>


        /// <summary>
        /// 获取指定字体的baseline
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <param name="fontRenderingEmSize"></param>
        /// <returns></returns>
        public static double GetBaseline(FontFamily fontFamily, double fontRenderingEmSize)
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

        public static double RefineValue(double i)
        {
            var value = IdealToRealWithNoRounding(RealToIdeal(i));

            if (i > 0)
            {
                // Non-zero values should not be converted to 0 accidentally through rounding, ensure that at least the min value is returned.
                value = Math.Max(value, DefaultIdealToReal);
            }

            return value;
        }

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

        private static readonly List<Point> DefaultGlyphOffsetArray = new List<Point>() {new Point()};

        private static readonly XmlLanguage DefaultXmlLanguage =
            XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.IetfLanguageTag);
    }
}
