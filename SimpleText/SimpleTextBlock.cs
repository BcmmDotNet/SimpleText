using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SimpleText
{
    internal class SimpleTextBlock : FrameworkElement
    {
        /// <summary>
        /// 段落集合。
        /// </summary>
        public ObservableCollection<SimpleParagraph> SimpleParagraphs { get; set; } = new ObservableCollection<SimpleParagraph>();
      
        protected override void OnRender(DrawingContext drawingContext)
        {
            // 暂时不考虑换行吧！只考虑段落。

            if (SimpleParagraphs.Count == 0)
            {
                return;
            }

            double y = 0;
            foreach (var paragraph in SimpleParagraphs)
            {
                
                try
                {
                    // 只做简单的绘制文本。
                    drawingContext.DrawSimpleParagraph(paragraph, new Point(0, y));
                }catch
                {
                    // 记录日志咯！
                }

                y += paragraph.LineSpacing + paragraph.FontSize;
            }
        }
    }
}
