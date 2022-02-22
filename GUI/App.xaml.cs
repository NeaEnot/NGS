using GUI.Views;
using System.Windows;

namespace GUI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string path = "";

            if (e.Args.Length == 1)
                path = e.Args[0];

            MainWindow wnd = new MainWindow(path);
            wnd.Show();
        }
    }
}
