using System;
using System.Linq;
using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace TodoListHelper.Models
{
    public class Todo : BindableBase
    {
        private string text = string.Empty;

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

        public string Text
        {
            get => text;
            set
            {
                if (SetProperty(ref text, value))
                {
                    RaisePropertyChanged(nameof(Completed));
                    RaisePropertyChanged(nameof(Title));
                    RaisePropertyChanged(nameof(AdditionalText));
                    RaisePropertyChanged(nameof(Working));
                }
            }
        }

        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Text))
                {
                    return string.Empty;
                }

                return Regex.Split(Text, @"\r\n|\r|\n").FirstOrDefault();
            }
        }

        public string AdditionalText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Text))
                {
                    return string.Empty;
                }

                var sps = Regex.Split(Text, @"\r\n|\r|\n");

                if (sps.Length <= 1)
                {
                    return string.Empty;
                }

                var ts = sps.Skip(1).Where(s => !string.IsNullOrWhiteSpace(s));
                return string.Join(Environment.NewLine, ts);
            }
        }

        public bool Working
        {
            get => Regex.IsMatch(Text, "\\*\\* *\\s*");
            set
            {
                if (IsCommentOnly)
                {
                    return;
                }

                Text = Regex.Replace(Text, " \\*\\*", string.Empty);
                Text = Regex.Replace(Text, "(^.*)([\\s$])", $"$1{(value ? " **" : string.Empty)}$2");
                RaisePropertyChanged();
            }
        }

        public bool IsCommentOnly { get; private set; }

        private static int CreatedCount { get; set; }

        /// <summary>
        /// Todo の Text の空行を除いた最後の行に入力したコメントを追記します。
        /// </summary>
        /// <param name="comment">コメントのテキスト</param>
        public void AddComment(string comment)
        {
            Text = new Regex("(.*)(\n*$)").Replace(Text, $"$1\n{comment}$2", 1);
        }
    }
}