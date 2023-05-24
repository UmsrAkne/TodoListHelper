using System.Configuration;
using System.IO;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TodoListHelper.Models;
using TodoListHelper.Views;

namespace TodoListHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private string title = "Prism Application";

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            ReloadTodo();
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

        public DisplayItemSelector DisplayItemSelector { get; } = new DisplayItemSelector();

        public DelegateCommand ShowSettingPageCommand => new DelegateCommand(() =>
        {
            dialogService.ShowDialog(nameof(SettingPage), new DialogParameters(), result => { ReloadTodo(); });
        });

        private void ReloadTodo()
        {
            var path = ConfigurationManager.AppSettings[App.TodoFilePathKeyName];

            if (!File.Exists(path))
            {
                return;
            }

            using (var sr = new StreamReader(path))
            {
                var parser = new Parser();
                DisplayItemSelector.RawTodos = parser.GetTodoList(sr.ReadToEnd());
            }
        }
    }
}