using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Mvvm;

namespace TodoListHelper.Models
{
    public class DisplayItemSelector : BindableBase
    {
        private List<Todo> rawTodos = new List<Todo>();
        private bool reverse;
        private bool showCompletedTodo = true;

        public ObservableCollection<Todo> Todos
        {
            get
            {
                var list = ShowCompletedTodo
                    ? RawTodos.Where(t => !t.Working)
                    : RawTodos.Where(t => !t.Completed && !t.Working);

                list = Reverse
                    ? list.OrderByDescending(t => t.Id)
                    : list.OrderBy(t => t.Id);

                return new ObservableCollection<Todo>(list);
            }
        }

        public ObservableCollection<Todo> WorkingTodos
        {
            get
            {
                return new ObservableCollection<Todo>(RawTodos.Where(t => t.Working));
            }
        }

        public List<Todo> RawTodos
        {
            private get => rawTodos;
            set
            {
                SetProperty(ref rawTodos, value);
                RaisePropertyChanged(nameof(Todos));
                RaisePropertyChanged(nameof(WorkingTodos));
                rawTodos = value;
            }
        }

        public bool Reverse { get => reverse; set => SetProperty(ref reverse, value); }

        public bool ShowCompletedTodo
        {
            get => showCompletedTodo;
            set
            {
                SetProperty(ref showCompletedTodo, value);
                RaisePropertyChanged(nameof(Todos));
            }
        }

        public void Add(Todo todo)
        {
            RawTodos.Insert(0, todo);
            todo.Id *= -1; // id を負の数にして、リストをソートした際にも一番上になるようにする。
            RaisePropertyChanged(nameof(Todos));
        }

        public void StartTodo(Todo todo)
        {
            Todo d = RawTodos.FirstOrDefault(t => t == todo);
            if (d == null)
            {
                return;
            }

            d.Working = true;
            RaisePropertyChanged(nameof(Todos));
            RaisePropertyChanged(nameof(WorkingTodos));
        }

        /// <summary>
        /// RawTodos に入っている Todo の Text プロパティを繋げた文字列を取得します。
        /// </summary>
        /// <returns>RawTodos の全要素の Text を結合した文字列</returns>
        public string GetText()
        {
            return string.Join(string.Empty, RawTodos.Select(t => t.Text));
        }
    }
}