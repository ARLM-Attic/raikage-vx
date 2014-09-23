using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace Raikage.Test.Core.Messeges
{
    public class TestMessage : MvxMessage
    {
        public string Message { get; set; }

        public TestMessage(object sender, string message)
            : base(sender)
        {
            this.Message = message;
        }
    }

    public class TestMessage2 : MvxMessage
    {
        public string Message { get; set; }

        public TestMessage2(object sender, string message)
            : base(sender)
        {
            this.Message = message;
        }
    }
}
