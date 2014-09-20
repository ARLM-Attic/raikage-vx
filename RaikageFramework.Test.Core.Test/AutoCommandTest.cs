using System;
using System.Diagnostics.Eventing.Reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raikage.Test.Core.ViewModels;
using Moq;
using AutoMoq;
using NUnit.Framework;
namespace RaikageFramework.Test.Core.Test
{
    [TestClass]
    public class AutoCommandTest
    {
        private AutoMoqer _mocker;
        private Mock<FirstViewModel> _firstViewModel;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMoqer();
            _firstViewModel = _mocker.GetMock<FirstViewModel>();
        }

        [Test]
        public void AutoCommandShouldBeInvokedIfNameConventionMatch()
        {
            var testCommand = _firstViewModel.Object.TestCommand;
            testCommand.Execute(10);
            _firstViewModel.Verify(m => m.Test(10), Times.Once());
        }
        [Test]
        public void AutoCommandShouldBeInvokedIfAttributeAdded()
        {
            var command1 = _firstViewModel.Object.Command1;
            command1.Execute("");
            _firstViewModel.Verify(m => m.Command1Method(""), Times.Once());
        }
    }
}
