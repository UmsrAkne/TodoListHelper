using NUnit.Framework;
using TodoListHelper.Models;

namespace TestTodoListHelper.Models
{
    public class TestTodo
    {
        private readonly string sampleText = "[ ] abc / def / todo / 30min\n" +
                                    "\tdescription\n";

        [Test]
        public void Todo生成テスト()
        {
            var t = new Todo(sampleText);
            Assert.AreEqual("[ ] abc / def / todo / 30min\n\tdescription\n", t.Text);
            Assert.AreEqual(1, t.Id);
            Assert.IsFalse(t.Completed);
            Assert.IsFalse(t.Working);
            Assert.IsFalse(t.IsCommentOnly);

            Assert.IsTrue(new Todo(" / comment only\n").IsCommentOnly);
        }
    }
}