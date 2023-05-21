using System.Text.RegularExpressions;
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

        [Test]
        public void Todoの完了処理テスト()
        {
            var todo = new Todo(sampleText);
            Assert.IsFalse(todo.Completed);
            todo.Completed = true;

            Assert.IsTrue(todo.Completed);
            Assert.AreEqual("[X] abc / def / todo / 30min\n\tdescription\n", todo.Text);
        }

        [Test]
        public void Todo作業中のテスト()
        {
            Assert.IsTrue(new Todo("[ ] abc / 30min **").Working);
            Assert.IsTrue(new Todo("[ ] abc / 30min **\n").Working);
            Assert.IsTrue(new Todo("[ ] abc / 30min **\n\n").Working);

            var todo = new Todo("[ ] abc / 30min\n");
            Assert.IsFalse(todo.Working);

            todo.Working = true;
            Assert.AreEqual("[ ] abc / 30min **\n", todo.Text);

            todo.Working = false;
            Assert.AreEqual("[ ] abc / 30min\n", todo.Text);
        }

        [Test]
        public void AddCommentTest()
        {
            var todo = new Todo("[ ] abc /\ntext\n");
            todo.AddComment("new comment");
            Assert.AreEqual("[ ] abc /\ntext\nnew comment\n", todo.Text);

            var todo2 = new Todo("[ ] abc /\ntext\n\n");
            todo2.AddComment("new comment");
            Assert.AreEqual("[ ] abc /\ntext\nnew comment\n\n", todo2.Text);

            var todo3 = new Todo("[ ] abc /\ntext");
            todo3.AddComment("new comment");
            Assert.AreEqual("[ ] abc /\ntext\nnew comment", todo3.Text);
        }

        [Test]
        public void Title取得テスト()
        {
            var todo = new Todo(sampleText);
            Assert.AreEqual("[ ] abc / def / todo / 30min", todo.Title, "複数行のテキストをもつ場合は一行目が使われる");

            var singleLineTodo = new Todo("[ ] singleLine /");
            Assert.AreEqual("[ ] singleLine /", singleLineTodo.Title, "Todoのテキストが１行ならばそのままのテキストが使われる");

            var emptyTodo = new Todo(string.Empty);
            Assert.AreEqual(string.Empty, emptyTodo.Title, "Text　が空白の場合は string.Empty が返る");
        }

        [Test]
        public void AdditionalText取得テスト()
        {
            var todo = new Todo("[ ] abc /\nddd");
            Assert.AreEqual("ddd", todo.AdditionalText, "普通に一行分説明がついてる場合");

            var twoLineTodo = new Todo("[ ] abc /\nddd\neee");
            Assert.AreEqual("ddd\r\neee", twoLineTodo.AdditionalText, "二行分説明がついてる場合");

            var noAdditionalTextTodo = new Todo("[ ] abc /");
            Assert.AreEqual(string.Empty, noAdditionalTextTodo.AdditionalText, "説明なしなので空文字");
        }
    }
}