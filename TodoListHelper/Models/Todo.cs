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

                // 全ての改行コードを一旦 \n に置き換える。
                Text = Regex.Replace(Text, @"\r\n|\r|\n", @"\n");
                Text = Regex.Replace(Text, " \\*\\*", string.Empty);

                // sp.First() では Text 内での最初の改行まで、または文字列全体が入る。
                var sp = Text.Split(new[] { @"\n" }, StringSplitOptions.None);
                Text = Text.Replace(sp.First(), sp.First() + (value ? " **" : string.Empty));

                // 最初に置き換えた改行コードを、環境に応じた定義されたコードに置き換える
                Text = Regex.Replace(Text, @"\\n", Environment.NewLine);
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

        /// <summary>
        /// このオブジェクトと同様の Text プロパティを持った複製を取得します。
        /// </summary>
        /// <returns>複製オブジェクトのプロパティに関して、 Completed, Working は false にセットされます。</returns>
        public Todo GetClone()
        {
            return new Todo(Text)
            {
                Completed = false,
                Working = false,
            };
        }
    }
}