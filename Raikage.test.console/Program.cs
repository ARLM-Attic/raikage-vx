using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Raikage.Test.Core.ViewModels;

namespace Raikage.test.console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var firstviewModel = new FirstViewModel();
            //var testCommand = firstviewModel.TestCommand;
            //testCommand.Execute("uu");

            var secondviewModel = new SecondViewModel(new MvxMessengerHub());
            secondviewModel.SendHello();
        }
    }
}
