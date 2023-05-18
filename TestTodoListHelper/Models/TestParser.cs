using NUnit.Framework;
using TodoListHelper.Models;

namespace TestTodoListHelper.Models
{
    [TestFixture]
    public class TestParser
    {
        private readonly string sampleText =
            "20230518 ------------------------------------" +
            "[ ] group1 / group2 / Sample todo 1 / 30min\n" +
            "\n" +
            "[ ] group1 / group2 / Sample todo 2 / 30min\n" +
            "\tdescription text\n" +
            "\n" +
            "[x] group1 / group2 / Sample todo 3 / 30min\n";

        [Test]
        public void GetTodoListTest()
        {
            var parser = new Parser();

            var todos = parser.GetTodoList(sampleText);
            Assert.AreEqual(3, todos.Count);
            Assert.AreEqual(todos[0], "[ ] group1 / group2 / Sample todo 1 / 30min\n\n");
            Assert.AreEqual(todos[1], "[ ] group1 / group2 / Sample todo 2 / 30min\n\tdescription text\n\n");
            Assert.AreEqual(todos[2], "[x] group1 / group2 / Sample todo 3 / 30min\n");
        }
    }
}