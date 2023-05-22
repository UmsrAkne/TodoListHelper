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
                    ? RawTodos.Where(t => !t.Completed)
                    : RawTodos.ToList();

                list = Reverse
                    ? list.OrderByDescending(t => t.Id)
                    : list.OrderBy(t => t.Id);

                return new ObservableCollection<Todo>(list);
            }
        }

        public List<Todo> RawTodos
        {
            private get => rawTodos;
            set
            {
                SetProperty(ref rawTodos, value);
                RaisePropertyChanged(nameof(Todos));
                rawTodos = value;
            }
        }

        public bool Reverse { get => reverse; set => SetProperty(ref reverse, value); }

        public bool ShowCompletedTodo { get => showCompletedTodo; set => SetProperty(ref showCompletedTodo, value); }
    }
}