using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging.Messages
{
    class CollideMessage : IMessage
    {
        public Entity Entity1
        {
            get;
            set;
        }

        public Entity Entity2
        {
            get;
            set;
        }
    }
}
