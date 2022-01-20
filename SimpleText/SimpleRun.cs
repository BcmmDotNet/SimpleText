using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SimpleText
{
    internal class SimpleRun
    {
        /// <summary>
        /// 象形字符集合。
        /// </summary>
        public Collection<GlyphChar> GlyphChars { get; set; }

        /// <summary>
        /// 行距。
        /// </summary>
        public int LineSpacing { get; set; }
    }
}
