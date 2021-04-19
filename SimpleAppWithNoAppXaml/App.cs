using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SimpleAppWithNoAppXaml
{
    public partial class App: Application
    {
        [STAThread]
        [System.Diagnostics.DebuggerNonUserCode]
        public static void Main()
        {
            App app = new App();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoadGlobalResourceDictionary("/Resources/GlobalResources.xaml");

            MainWindow wnd = new();
            wnd.Show();
        }

        public void LoadGlobalResourceDictionary(string relativeUri)
        {
            Uri uri = new Uri(relativeUri, UriKind.Relative);
            var resourceDictionary = (ResourceDictionary)Application.LoadComponent(uri);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

    }
}
