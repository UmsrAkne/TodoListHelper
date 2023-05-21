using System.Configuration;
using System.Linq;
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
        public static readonly string TodoFilePathKeyName = "TodoFilePath";

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SettingPage, SettingPageViewModel>();
        }

        protected override void Initialize()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!config.AppSettings.Settings.AllKeys.Contains(TodoFilePathKeyName))
            {
                config.AppSettings.Settings.Add(TodoFilePathKeyName, string.Empty);
                config.Save();
            }

            base.Initialize();
        }
    }
}