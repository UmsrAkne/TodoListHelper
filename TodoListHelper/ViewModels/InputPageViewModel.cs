using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TodoListHelper.Models;

namespace TodoListHelper.ViewModels
{
    public class InputPageViewModel : BindableBase, IDialogAware
    {
        private string inputText = string.Empty;

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public Todo Todo { get; private set; }

        public string InputText { get => inputText; set => SetProperty(ref inputText, value); }

        public DelegateCommand CloseCommand => new DelegateCommand(() =>
        {
            var result = new DialogResult(ButtonResult.OK);
            result.Parameters.Add(nameof(InputText), InputText);
            RequestClose?.Invoke(result);
        });

        public DelegateCommand CancelCommand => new DelegateCommand(() =>
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        });

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Todo = parameters.GetValue<Todo>(nameof(Todo));
            RaisePropertyChanged(nameof(Todo));
        }
    }
}