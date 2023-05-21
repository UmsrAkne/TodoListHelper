using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TodoListHelper.Models;
using TodoListHelper.Views;

namespace TodoListHelper.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private readonly IDialogService dialogService;
        private ObservableCollection<Todo> todos = new ObservableCollection<Todo>();

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            ReloadTodo();
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<Todo> Todos { get => todos; set => SetProperty(ref todos, value); }

        public DelegateCommand ShowSettingPageCommand => new DelegateCommand(() =>
        {
            dialogService.ShowDialog(nameof(SettingPage), new DialogParameters(), result => { ReloadTodo(); });
        });

        private void ReloadTodo()
        {
            var path = ConfigurationManager.AppSettings[App.todoFilePathKeyName];

            if (!File.Exists(path))
            {
                return;
            }

            using (var sr = new StreamReader(path))
            {
                var parser = new Parser();
                Todos = new ObservableCollection<Todo>(parser.GetTodoList(sr.ReadToEnd()));
            }
        }
    }
}