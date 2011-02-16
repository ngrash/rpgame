using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging.Messages
{
    class SpawnMessage : IMessage
    {
        public Entity EntityToSpawn
        {
            get;
            set;
        }
    }
}
