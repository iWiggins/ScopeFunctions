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
    public class LetScopeTests
    {
        [TestMethod()]
        public void Let1InvokesOnNonNull()
        {
            string? str = "str";
            string? inner = "inner";
            bool invoked = false;
            string? invoke(string? s)
            {
                invoked = true;
                return inner;
            }
            string? ret = str.Let(invoke);
            Assert.IsTrue(invoked);
            Assert.AreEqual(ret, inner);
        }

        [TestMethod()]
        public void Let1DoesNotInvokeOnNull()
        {
            string? str = null;
            string? inner = "inner";
            string? invoke(string? s)
            {
                Assert.Fail();
                return inner;
            }
            string? ret = str.Let(invoke);
            Assert.IsNull(ret);
        }

        [TestMethod()]
        public void Let11InvokesOnNonNull()
        {
            string? str = "str";
            string? inner = "inner";
            string? outer = "outer";
            bool invoked = false;
            string? invoke(string? s)
            {
                invoked = true;
                return inner;
            }
            string? ret = str.Let(invoke, outer);
            Assert.IsTrue(invoked);
            Assert.AreEqual(ret, inner);
        }

        [TestMethod()]
        public void Let11DoesNotInvokeOnNull()
        {
            string? str = null;
            string? inner = "inner";
            string? outer = "outer";
            string? invoke(string? s)
            {
                Assert.Fail();
                return inner;
            }
            string? ret = str.Let(invoke, outer);
            Assert.AreEqual(ret, outer);
        }

        [TestMethod()]
        public void Let2InvokesFirstOnNonNull()
        {
            string? str = "str";
            string? inner = "inner";
            bool invoked = false;
            string? first(string? s)
            {
                invoked = true;
                return inner;
            }
            string? second(string? s)
            {
                Assert.Fail();
                return null;
            }
            string? ret = str.Let(first, second);
            Assert.IsTrue(invoked);
            Assert.AreEqual(ret, inner);
        }

        [TestMethod()]
        public void Let2InvokesSecondOnNull()
        {
            string? str = null;
            string? inner = "inner";
            bool invoked = false;
            string? first(string? s)
            {
                Assert.Fail();
                return null;
            }
            string? second(string? s)
            {
                invoked = true;
                return inner;
            }
            string? ret = str.Let(first, second);
            Assert.IsTrue(invoked);
            Assert.AreEqual(ret, inner);
        }
    }
}