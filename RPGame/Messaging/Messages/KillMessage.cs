using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging.Messages
{
    class KillMessage : IMessage
    {
        public Entity EntityToKill
        {
            get;
            set;
        }
    }
}
