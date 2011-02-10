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
                    Entity.ProcessMessage(new StopMovingMessage());
                    this.isMoving = false;
                }
                else
                {
                    Direction newDirection = Direction.Down;

                    switch (keyboardEvent.Key)
                    {
                        case Key.UpArrow:
                            this.isMoving = true;
                            newDirection = Direction.Up;
                            break;
                        case Key.DownArrow:
                            this.isMoving = true;
                            newDirection = Direction.Down;
                            break;
                        case Key.LeftArrow:
                            this.isMoving = true;
                            newDirection = Direction.Left;
                            break;
                        case Key.RightArrow:
                            this.isMoving = true;
                            newDirection = Direction.Right;
                            break;
                        case Key.Space:
                            Attack();
                            break;
                    }

                    if (this.isMoving)
                    {
                        Entity["DIRECTION"] = newDirection;
                        Entity.ProcessMessage(new StartMovingMessage());
                    }
                }
            }
        }

        void Attack()
        {
            Entity hit = new Entity();
            hit.Features.Add(new HitFeature());
            //hit.Features.Add(new CollidableFeature());
        }

        public void Update(float timeElapsed)
        {
            
        }
    }
}
