using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TodoListHelper.Models
{
    public class Parser
    {
        /// <summary>
        /// 入力したテキストからパース可能な全ての Todo と、それに付随するテキストをリストにして取得します。
        /// </summary>
        /// <param name="text">フォーマットに沿って記述された Todoリスト</param>
        /// <returns>角括弧を基準に分割された Todo のリスト</returns>
        public List<Todo> GetTodoList(string text)
        {
            var lastParentId = 0;
            var rep = Regex.Replace(text, "\t*\\[.\\]", ",,,,$0");
            return Regex.Split(rep, ",,,,").Select(s =>
            {
                var todo = new Todo(s);

                if (Regex.IsMatch(todo.Title, "^\t+\\[.\\]"))
                {
                    todo.ParentId = lastParentId;
                }
                else
                {
                    lastParentId = todo.Id;
                }

                return todo;
            }).Where(t => t.Text != string.Empty).ToList();
        }
    }
}