﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Messaging.Messages
{
    class SetFocusMessage : IMessage
    {
        public Entity TargetEntity
        {
            get;
            set;
        }
    }
}
