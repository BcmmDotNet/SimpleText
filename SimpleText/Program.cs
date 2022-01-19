using System;
using System.Windows;
using System.Windows.Documents;

namespace SimpleText
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application application = new Application();
            application.Run(new MainWindow());
        }
    }
}
