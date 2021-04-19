# WpfNoAppXaml

This project demonstrate how to get rid of the `App.xaml` file and restore the `main` entry point in a WPF application.

There are several reasons for doing this:
- being able to take control of the application bootstrap
- using Dependency Injection in WPF (not demonstrated in this project but already tested successfully)
- controlling resources in Depenency Injection


This project refers to .NET WPF 3.1, 5.0 and newer (.NET Core).

1. Start from a new WPF .NET project
2. Delete `App.xaml` and `App.xaml.cs`
3. Create a class named `App.cs` (this name is not really important)

```    public partial class App: Application
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
```

4. Create a folder named `Resources`
5. Create resource dictionary named `GlobalResources.xaml` inside the `Resources` folder. This dictionary contains a number of style, templates and other typical WPF "global" resources:

```
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Style x:Key="toolBarButtonImage" TargetType="ButtonBase">
        <!-- ... -->
    </Style>

</ResourceDictionary>
```

6. Create a `Properties` folder
7. Create a resource dictionary named `DesignTimeResources.xaml` inside the `Properties` folder
> This file contains the resources used from the Visual Studio designer. You can change the file name if you want.
> It is fundamental the folder is called `Properties` otherwise it won't work

8. The `DesignTimeResources.xaml` just contains a reference to the other resource dictionaries you already use at runtime
```
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="/Resources/GlobalResources.xaml" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```

9. Modify the `csproj` file in order to instruct the designer to load the resources:

```
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net5.0-windows</TargetFramework>
        <UseWPF>true</UseWPF>
    </PropertyGroup>

    <ItemGroup>
        <None Include="Properties\DesignTimeResources.xaml">
            <SubType>Designer</SubType>
            <Generator>MSBuild:Compile</Generator>
        </None>
    </ItemGroup>

</Project>
```
