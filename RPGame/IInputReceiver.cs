using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging.Messages;

namespace RPGame
{
    interface IInputReceiver
    {
        InputSystem InputSystem { set; get; }
        void HandleInput(UserInputMessage userInputMessage);
    }
}
