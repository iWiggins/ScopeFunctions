using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScopeFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopeFunctions.Tests
{
    [TestClass()]
    public class ApplyScopeTests
    {
        [TestMethod()]
        public void Apply1InvokesOnNonNull()
        {
            bool invoked = false;
            string? str = "";
            string? ret = str.Apply(s => invoked = true);
            Assert.IsTrue(invoked);
            Assert.AreEqual(ret, str);
        }

        [TestMethod()]
        public void Appli1DoesNotInvokeOnNull()
        {
            string? str = null;
            string? ret = str.Apply(s => Assert.Fail());
            Assert.AreEqual(ret, str);
        }

        [TestMethod()]
        public void Apply2InvokesFirstOnNonNull()
        {
            bool invoked = false;
            string? str = "";
            string? ret = str.Apply(s => invoked = true, Assert.Fail);
            Assert.IsTrue(invoked);
            Assert.AreEqual(ret, str);
        }

        [TestMethod()]
        public void Apply2InvokesSecondOnNull()
        {
            bool invoked = false;
            string? str = null;
            string? ret = str.Apply(s => Assert.Fail(), () => invoked = true);
            Assert.IsTrue(invoked);
            Assert.AreEqual(ret, str);
        }
    }
}