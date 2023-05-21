using System.Collections.ObjectModel;
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

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<Todo> Todos { get; set; } = new ObservableCollection<Todo>();

        public DelegateCommand ShowSettingPageCommand => new DelegateCommand(() =>
        {
            dialogService.ShowDialog(nameof(SettingPage), new DialogParameters(), result => { });
        });
    }
}