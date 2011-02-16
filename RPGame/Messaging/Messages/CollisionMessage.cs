using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging.Messages
{
    class CollisionMessage : IMessage
    {
        public Entity WithEntity
        {
            get;
            set;
        }

        public override string ToString()
        {
            return WithEntity.ToString();
        }
    }
}
