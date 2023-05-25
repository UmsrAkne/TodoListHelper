using NUnit.Framework;
using TodoListHelper.Models;

namespace TestTodoListHelper.Models
{
    public class TestDisplayItemSelector
    {
        private string sampleTodoText = "[ ] test / abc1 /\n\n" +
                                        "[ ] test / abc2 /\n" +
                                        "\tdescription\n\n" +
                                        "[ ] test / abc3";

        [Test]
        public void GetTextTest()
        {
            var todos = new Parser().GetTodoList(sampleTodoText);
            var selector = new DisplayItemSelector
            {
                RawTodos = todos,
            };

            Assert.AreEqual(sampleTodoText, selector.GetText(), "内容に変更を加えていなければ、変換前と同じテキストが生成されるはず");
        }

        [Test]
        public void AddTest()
        {
            var selector = new DisplayItemSelector()
            {
                RawTodos = new Parser().GetTodoList(sampleTodoText),
            };

            selector.Add(new Todo("added Todo\n"));
            Assert.AreEqual($"added Todo\n{sampleTodoText}", selector.GetText(), "新しく加えた Todo は先頭に追加される");
        }
    }
}