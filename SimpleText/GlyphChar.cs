using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SimpleText
{
    /// <summary>
    /// 一个不可边得数据模型 GlyphChar 。
    /// </summary>
    internal record GlyphChar
    {
        /// <summary>
        /// 需要呈现的 Unicode 字符。
        /// </summary>
        public char? UnicodeChar { get; init; } = default;

        /// <summary>
        /// 字体权重，默认值 Normal。
        /// </summary>
        public FontWeight FontWeight { get; init; } = FontWeights.Normal;

        /// <summary>
        /// 字体样式，默认值 Normal。
        /// </summary>
        public FontStyle FontStyle { get; init; } = FontStyles.Normal;

        /// <summary>
        /// 字体大小，默认值 30。
        /// </summary>
        public int FontSize { get; init; } = 30;

        /// <summary>
        /// 字体颜色，默认值 Black。
        /// </summary>
        public Brush? Foreground { get; init; } = Brushes.Black;

        /// <summary>
        /// 字体背景色，默认为透明。
        /// </summary>
        public Brush? Background { get; set; } = Brushes.Transparent;

        /// <summary>
        /// 字体族，默认值“微软雅黑”。
        /// </summary>
        public string? FontFamily { get; init; } = "微软雅黑";
    }
}
