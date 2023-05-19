using System.Collections.ObjectModel;
using Prism.Mvvm;
using TodoListHelper.Models;

namespace TodoListHelper.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<Todo> Todos { get; set; } = new ObservableCollection<Todo>();
    }
}