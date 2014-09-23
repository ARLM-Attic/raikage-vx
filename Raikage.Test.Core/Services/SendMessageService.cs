using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using Raikage.Test.Core.Messeges;

namespace Raikage.Test.Core.Services
{
    public interface ISendMessageService
    {
        void SendHello();
        void SendWorld();
    }
    public class SendMessageService : ISendMessageService
    {
        private readonly IMvxMessenger _messenger;

        public SendMessageService(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public void SendHello()
        {
            _messenger.Publish(new TestMessage(this, "hello"));
        }
        public void SendWorld()
        {
            _messenger.Publish(new TestMessage2(this, "World"));
        }
    }
}
