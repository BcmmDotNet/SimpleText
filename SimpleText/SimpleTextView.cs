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
                new GlyphChar(){UnicodeChar = 'H'},
                new GlyphChar(){UnicodeChar = 'E'},
                new GlyphChar(){UnicodeChar = 'L'},
                new GlyphChar(){UnicodeChar = 'L'},
                new GlyphChar(){UnicodeChar = 'O'},
                new GlyphChar(){UnicodeChar = '!'},
                new GlyphChar(){UnicodeChar = '\r'},
                new GlyphChar(){UnicodeChar = 'W'},
                new GlyphChar(){UnicodeChar = 'O'},
                new GlyphChar(){UnicodeChar = 'R'},
                new GlyphChar(){UnicodeChar = 'D'},
                new GlyphChar(){UnicodeChar = '!'},
            };

            return glyphChars;
        }
    }
}
