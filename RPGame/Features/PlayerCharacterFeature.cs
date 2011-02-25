using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;
using SdlDotNet.Input;

namespace RPGame.Features
{
    class PlayerCharacterFeature : Feature, IMessageReceiver
    {
        bool isMoving = false;

        public void ReceiveMessage(IMessage message)
        {
            if (message is UserInputMessage)
            {
                KeyboardEventArgs keyboardEvent = ((UserInputMessage)message).KeyboardEvent;

                if (this.isMoving && !keyboardEvent.Down)
                {
                    Key key = keyboardEvent.Key;

                    if (DirectionHelper.GetDirectionFromKey(key) == Entity.Attributes.Get<Direction>("DIRECTION"))
                    {
                        Entity.ProcessMessage(new StopMovingMessage());
                        this.isMoving = false;
                    }
                }
                else if (keyboardEvent.Down)
                {
                    Direction newDirection = DirectionHelper.GetDirectionFromKey(keyboardEvent.Key);
                    if (newDirection != Direction.None)
                    {
                        this.isMoving = true;

                        Entity.Attributes.Set<Direction>("DIRECTION", newDirection);
                        Entity.ProcessMessage(new StartMovingMessage());
                    }
                    // irgendeine Taste
                    else
                    {
                        switch (keyboardEvent.Key)
                        {
                            case Key.Space:
                                Entity.ProcessMessage(new AttackMessage());
                                break;
                        }
                    }
                }
            }
        }
    }
}
