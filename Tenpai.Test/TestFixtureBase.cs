using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Test
{
    [TestFixture]
    public class TestFixtureBase
    {
        [SetUp]
        public void SetUp()
        {
            Console.WriteLine($"BEGIN {NUnit.Framework.TestContext.CurrentContext.Test.FullName}()");
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine($"END {NUnit.Framework.TestContext.CurrentContext.Test.FullName}()");
        }
    }
}
