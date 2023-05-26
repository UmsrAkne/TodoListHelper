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
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private GitManager gitManager;
        private string title = "Prism Application";

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            ReloadTodo();

            var todoFilePath = ConfigurationManager.AppSettings[App.TodoFilePathKeyName];
            var repoPath = ConfigurationManager.AppSettings[App.RepositoryPathKeyName];
            if (Directory.Exists(repoPath) && File.Exists(todoFilePath))
            {
                gitManager = new GitManager(repoPath)
                {
                    CurrentFilePath = todoFilePath,
                };
            }
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

        public DisplayItemSelector DisplayItemSelector { get; } = new DisplayItemSelector();

        public DelegateCommand ShowSettingPageCommand => new DelegateCommand(() =>
        {
            dialogService.ShowDialog(nameof(SettingPage), new DialogParameters(), result => { ReloadTodo(); });
        });

        public DelegateCommand<Todo> CloneTodoCommand => new DelegateCommand<Todo>(todo =>
        {
            AddTodo(todo.GetClone());
        });

        private void AddTodo(Todo todo)
        {
            DisplayItemSelector.Add(todo);

            var path = ConfigurationManager.AppSettings[App.TodoFilePathKeyName];
            if (!File.Exists(path))
            {
                return;
            }

            File.WriteAllText(path, DisplayItemSelector.GetText(), Encoding.UTF8);
            gitManager?.TodoAdditionCommit(todo);
        }

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