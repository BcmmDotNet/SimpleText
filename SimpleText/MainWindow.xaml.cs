using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleText
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Paragraph.Inlines.Add(new Run("Hello \nWorld"));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TextPointer start = FlowDocument.ContentStart;
            TextPointer end = FlowDocument.ContentEnd;
        }
    }
}
