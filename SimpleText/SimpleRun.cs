using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;

namespace SimpleText
{
    internal record SimpleRun
    {
        /// <summary>
        /// 象形字符集合。
        /// </summary>
        public Collection<GlyphChar>? GlyphChars { get; init; } = default;

        /// <summary>
        /// 行距。
        /// </summary>
        public int LineSpacing { get; init; } = default;
    }
}
