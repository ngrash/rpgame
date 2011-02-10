using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Input;

namespace RPGame.Messaging.Messages
{
    class UserInputMessage : IMessage
    {
        public KeyboardEventArgs KeyboardEvent
        {
            get;
            set;
        }
    }
}
