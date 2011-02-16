using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;

namespace RPGame.Features
{
    class DieOnCollisonFeature : Feature, IMessageReceiver
    {
        public void ReceiveMessage(IMessage message)
        {
            if (message is CollisionMessage)
            {
                Entity.ProcessMessage(new KillMessage() { EntityToKill = Entity });
            }
        }
    }
}
