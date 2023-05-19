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
        public List<string> GetTodoList(string text)
        {
            var rep = Regex.Replace(text, "\t*\\[.\\]", ",,,,$0");
            return Regex.Split(rep, ",,,,").Where(s => Regex.IsMatch(s, "\\[.\\]")).ToList();
        }
    }
}