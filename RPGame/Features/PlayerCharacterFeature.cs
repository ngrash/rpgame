using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;
using SdlDotNet.Input;

namespace RPGame.Features
{
    class PlayerCharacterFeature : IFeature, IMessageReceiver
    {
        bool isMoving = false;

        public Entity Entity
        {
            get;
            set;
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is UserInputMessage)
            {
                KeyboardEventArgs keyboardEvent = ((UserInputMessage)message).KeyboardEvent;

                if (this.isMoving && !keyboardEvent.Down)
                {
                    Key key = keyboardEvent.Key;

                    if (GetDirectionFromKey(key) == (Direction)Entity["DIRECTION"])
                    {
                        Entity.ProcessMessage(new StopMovingMessage());
                        this.isMoving = false;
                    }
                }
                else if (keyboardEvent.Down)
                {
                    Direction newDirection = GetDirectionFromKey(keyboardEvent.Key);
                    if (newDirection != Direction.None)
                    {
                        this.isMoving = true;

                        Entity["DIRECTION"] = newDirection;
                        Entity.ProcessMessage(new StartMovingMessage());
                    }
                }
            }
        }

        Direction GetDirectionFromKey(Key key)
        {
            Direction newDirection = Direction.None;

            switch (key)
            {
                case Key.UpArrow:
                    newDirection = Direction.Up;
                    break;
                case Key.DownArrow:
                    newDirection = Direction.Down;
                    break;
                case Key.LeftArrow:
                    newDirection = Direction.Left;
                    break;
                case Key.RightArrow:
                    newDirection = Direction.Right;
                    break;
            }

            return newDirection;
        }

        public void Update(float timeElapsed)
        {
            
        }
    }
}
