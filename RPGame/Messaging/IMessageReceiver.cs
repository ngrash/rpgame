using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging
{
    interface IMessageReceiver
    {
        void ReceiveMessage(IMessage message);
    }
}
