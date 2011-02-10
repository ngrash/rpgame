using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging
{
    interface IMessageChannel
    {
        event Action<IMessage> MessageIncoming;
        void ProcessMessage(IMessage message);
    }
}
