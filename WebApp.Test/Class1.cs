using NUnit.Framework;

namespace WebApp.Test
{
    [TestFixture]
    public class Class1
    {
        [TestCase(1, 1, 2)]
        public void PassingTest(int x, int y, int expected)
        {
            Assert.That(this.Add(x, y), Is.EqualTo(expected));
        }

        [TestCase(1, 2, 2)]
        public void FailingTest(int x, int y, int expected)
        {
            Assert.That(this.Add(x, y), Is.EqualTo(expected));
        }

        private int Add(int x, int y)
        {
            return x + y;
        }
    }
}
