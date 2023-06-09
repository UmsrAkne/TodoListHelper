﻿using System.Configuration;
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
        public static readonly string RepositoryPathKeyName = "RepositoryPathKey";

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SettingPage, SettingPageViewModel>();
            containerRegistry.RegisterDialog<InputPage, InputPageViewModel>();
        }

        protected override void Initialize()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!config.AppSettings.Settings.AllKeys.Contains(TodoFilePathKeyName))
            {
                config.AppSettings.Settings.Add(TodoFilePathKeyName, string.Empty);
            }

            if (!config.AppSettings.Settings.AllKeys.Contains(RepositoryPathKeyName))
            {
                config.AppSettings.Settings.Add(RepositoryPathKeyName, string.Empty);
            }

            config.Save();

            base.Initialize();
        }
    }
}