using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TodoListHelper.Models;

namespace TestTodoListHelper.Models
{
    [TestFixture]
    public class TestParser
    {
        private readonly string sampleText =
            "20230518 ------------------------------------\n" +
            "[ ] group1 / group2 / Sample todo 1 / 30min\n" +
            "\n" +
            "[ ] group1 / group2 / Sample todo 2 / 30min\n" +
            "\tdescription text\n" +
            "\n" +
            "[x] group1 / group2 / Sample todo 3 / 30min\n" +
            "\t[ ] group1 / group2 / Sub todo / 15min\n\n" +

            "20230517 ------------------------------------\n\n" +

            "[ ] group1 / group2 / Sample todo 2 / 30min\n";

        [Test]
        public void GetTodoListTest()
        {
            var parser = new Parser();

            var todos = parser.GetTodoList(sampleText).Select(t => t.Text).ToList();
            Assert.AreEqual(7, todos.Count);

            var ss = new []
            {
                "20230518 ------------------------------------\n",
                "[ ] group1 / group2 / Sample todo 1 / 30min\n\n",
                "[ ] group1 / group2 / Sample todo 2 / 30min\n\tdescription text\n\n",
                "[x] group1 / group2 / Sample todo 3 / 30min\n",
                "\t[ ] group1 / group2 / Sub todo / 15min\n\n",
                "20230517 ------------------------------------\n\n",
                "[ ] group1 / group2 / Sample todo 2 / 30min\n",
            };

            CollectionAssert.AreEqual(todos, ss);
        }

        [Test]
        public void ParentIdTest()
        {
            var parser = new Parser();
            var parentIds = parser.GetTodoList(sampleText).Select(todo => todo.ParentId);


            CollectionAssert.AreEqual(
                new List<int>() { 0, 0, 0, 0, 4 }, parentIds
            );
        }
    }
}