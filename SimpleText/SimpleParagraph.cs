using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SimpleText
{
    public class SimpleParagraph : DependencyObject
    {
        /// <summary>
        /// 字体权重。
        /// </summary>
        public FontWeight Weight
        {
            get { return (FontWeight)GetValue(WeightProperty); }
            set { SetValue(WeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Weight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeightProperty =
            DependencyProperty.Register("Weight", typeof(FontWeight), typeof(SimpleParagraph), new PropertyMetadata(FontWeights.Normal));

        /// <summary>
        /// 字体样式。
        /// </summary>
        public FontStyle Style
        {
            get { return (FontStyle)GetValue(StyleProperty); }
            set { SetValue(StyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Style.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StyleProperty =
            DependencyProperty.Register("Style", typeof(FontStyle), typeof(SimpleParagraph), new PropertyMetadata(FontStyles.Normal));

        /// <summary>
        /// 字体颜色。
        /// </summary>
        public int FontSize
        {
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(int), typeof(SimpleParagraph), new FrameworkPropertyMetadata(12, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// 文本。
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SimpleParagraph), new PropertyMetadata(null));

        /// <summary>
        /// 字体颜色。
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foreground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(SimpleParagraph), new PropertyMetadata(null));

        /// <summary>
        /// 行距。
        /// </summary>
        public int LineSpacing
        {
            get { return (int)GetValue(LineSpacingProperty); }
            set { SetValue(LineSpacingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineSpacing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineSpacingProperty =
            DependencyProperty.Register("LineSpacing", typeof(int), typeof(SimpleParagraph), new PropertyMetadata(0));

        /// <summary>
        /// 字体族。
        /// </summary>
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(string), typeof(SimpleParagraph), new PropertyMetadata("微软雅黑"));
    }
}
