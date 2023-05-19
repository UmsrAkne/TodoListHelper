using System.Text.RegularExpressions;

namespace TodoListHelper.Models
{
    public class Todo
    {
        public Todo(string text)
        {
            Text = text;
            IsCommentOnly = !Regex.IsMatch(text, "\\[.\\]");
            Id = ++CreatedCount;
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public bool Completed { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool Working { get; set; }

        public bool IsCommentOnly { get; private set; }

        private static int CreatedCount { get; set; }
    }
}