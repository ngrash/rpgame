using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging.Messages
{
    class ExperienceMessage : IMessage
    {
        public Entity Target
        {
            get;
            set;
        }

        public float Experience
        {
            get;
            set;
        }
    }
}
