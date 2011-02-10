using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;

namespace RPGame.Features
{
    class AttackFeature : Feature, IMessageReceiver
    {
        CollisionSystem collisionSystem;

        public AttackFeature(CollisionSystem collisionSystem)
        {

        }

        public override void Update(float timeElapsed)
        {
            throw new NotImplementedException();
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is AttackMessage)
            {
                Direction currentDirection = (Direction)Entity["DIRECTION"];

                // spawn hit entity in current direction
            }
        }
    }
}
