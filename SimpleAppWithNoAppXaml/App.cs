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

        public void LoadGlobalResourceDictionary(Uri uri)
        {
            var resourceDictionary = LoadResourceDictionary(uri);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        public void LoadGlobalResourceDictionary(string relativeUri)
        {
            var resourceDictionary = LoadResourceDictionary(relativeUri);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativeUri">A relative uri like /Resources/myres.xaml</param>
        public ResourceDictionary LoadResourceDictionary(string relativeUri)
        {
            Uri uri = new Uri(relativeUri, UriKind.Relative);
            return LoadResourceDictionary(uri);
        }

        public ResourceDictionary LoadResourceDictionary(Uri uri)
        {
            return (ResourceDictionary)Application.LoadComponent(uri);
            //var info = Application.GetResourceStream(uri);
            //System.Windows.Markup.XamlReader reader = new System.Windows.Markup.XamlReader();
            //return (ResourceDictionary)reader.LoadAsync(info.Stream);
        }
    }
}
