using System;
using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace TodoListHelper.Models
{
    public class Todo : BindableBase
    {
        public Todo(string text)
        {
            Text = text;
            IsCommentOnly = !Regex.IsMatch(text, "\\[.\\]");
            Id = ++CreatedCount;
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public bool Completed
        {
            get => Regex.IsMatch(Text, "\\[x\\]", RegexOptions.IgnoreCase);
            set
            {
                if (IsCommentOnly)
                {
                    return;
                }

                Text = Regex.Replace(Text, "\\[.\\]", $"[{(value ? "X" : " ")}]");
                RaisePropertyChanged();
            }
        }

        public string Text { get; set; } = string.Empty;

        public bool Working { get; set; }

        public bool IsCommentOnly { get; private set; }

        private static int CreatedCount { get; set; }
    }
}