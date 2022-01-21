using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SimpleText
{
    internal class SimpleTextView : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            var simpleRun = new SimpleRun() {GlyphChars = GetDefaultGlyphChars(), LineSpacing = 5};
            drawingContext.DrawSimpleRun(simpleRun);
        }

        private Collection<GlyphChar> GetDefaultGlyphChars()
        {
            Collection<GlyphChar> glyphChars = new Collection<GlyphChar>
            {
                new GlyphChar(){UnicodeChar = 'H', Background = Brushes.Red, Foreground = Brushes.Blue, FontFamily = "微软雅黑"},
                new GlyphChar(){UnicodeChar = 'E', Background = Brushes.Green, Foreground = Brushes.Aqua, FontFamily = "等线"},
                new GlyphChar(){UnicodeChar = '我', Background = Brushes.Blue, Foreground = Brushes.BlueViolet, FontFamily = "宋体"},
                new GlyphChar(){UnicodeChar = 'L', Background = Brushes.Yellow, Foreground = Brushes.Brown, FontFamily = "Arial"},
                new GlyphChar(){UnicodeChar = '是', Background = Brushes.Aqua, Foreground = Brushes.Chocolate, FontFamily = "Cambria"},
                new GlyphChar(){UnicodeChar = '!', Background = Brushes.BlueViolet, Foreground = Brushes.DarkKhaki, FontFamily = "Cambria"},
                new GlyphChar(){UnicodeChar = '\r',},
                new GlyphChar(){UnicodeChar = 'W', Background = Brushes.Brown, Foreground = Brushes.DarkSalmon, FontFamily = "Arial"},
                new GlyphChar(){UnicodeChar = '发', Background = Brushes.Chocolate, Foreground = Brushes.Gold, FontFamily = "宋体"},
                new GlyphChar(){UnicodeChar = 'R', Background = Brushes.DarkKhaki, Foreground = Brushes.Aquamarine, FontFamily = "等线"},
                new GlyphChar(){UnicodeChar = '菜', Background = Brushes.DarkSalmon, Foreground = Brushes.Beige, FontFamily = "微软雅黑"},
                new GlyphChar(){UnicodeChar = '!', Background = Brushes.Gold, Foreground = Brushes.CadetBlue, FontFamily = "微软雅黑"},
            };

            return glyphChars;
        }
    }
}
