using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;
using System.Drawing;

namespace RPGame.Features
{
    class MoveFeature : Feature, IMessageReceiver
    {
        bool isMoving = false;
        Direction movingDirection;

        public void ReceiveMessage(IMessage message)
        {
            if (message is StartMovingMessage)
            {
                this.isMoving = true;
                this.movingDirection = Entity.Attributes.Get<Direction>("DIRECTION");
            }
            else if (message is StopMovingMessage)
            {
                this.isMoving = false;
            }
        }

        public override void Update(float timeElapsed)
        {
            if (this.isMoving)
            {
                int speed = Entity.Attributes.Get<int>("SPEED");
                Entity.Move(this.movingDirection, (int)speed);
            }
        }
    }
}
