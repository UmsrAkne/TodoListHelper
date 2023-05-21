using System.Windows;
using Prism.Ioc;
using TodoListHelper.ViewModels;
using TodoListHelper.Views;

namespace TodoListHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly string todoFilePathKeyName = "TodoFilePath";

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SettingPage, SettingPageViewModel>();
        }
    }
}