﻿using Prism.Mvvm;

namespace TodoListHelper.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}