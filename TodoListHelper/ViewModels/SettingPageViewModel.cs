using System;
using System.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace TodoListHelper.ViewModels
{
    public class SettingPageViewModel : BindableBase, IDialogAware
    {
        private string todoFilePath;
        private string repositoryPath;

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public string TodoFilePath
        {
            get => todoFilePath;
            set
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[App.TodoFilePathKeyName].Value = value;
                config.Save();
                SetProperty(ref todoFilePath, value);
            }
        }

        public string RepositoryPath
        {
            get => repositoryPath;
            set
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[App.RepositoryPathKeyName].Value = value;
                config.Save();
                SetProperty(ref repositoryPath, value);
            }
        }

        public DelegateCommand CloseCommand => new DelegateCommand(() =>
        {
            RequestClose?.Invoke(new DialogResult());
        });

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            TodoFilePath = ConfigurationManager.AppSettings[App.TodoFilePathKeyName];
        }
    }
}