using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raikage.Test.Core.ViewModels;

namespace RaikageFramework.Test.Core.Test
{
    [TestClass]
    public class AutoCommandTest
    {
        [TestMethod]
        public void AutoCommandShouldBeInvokedIfNameConventionMatch()
        {
            var firstviewModel = new FirstViewModel();
            var testCommand = firstviewModel.TestCommand;
            testCommand.Execute(10);
            Assert.AreEqual(10, firstviewModel.TestCount);
        }
    }
}
