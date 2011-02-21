using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging.Messages
{
    class LevelUpMessage : IMessage
    {
        public Entity Target
        {
            get;
            set;
        }

        public int NewLevel
        {
            get;
            set;
        }
    }
}
