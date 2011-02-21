using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;
using System.Drawing;

namespace RPGame.Features
{
    class AttackFeature : Feature, IMessageReceiver
    {
        CollisionSystem collisionSystem;

        public AttackFeature(CollisionSystem collisionSystem)
        {
            this.collisionSystem = collisionSystem;
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is AttackMessage)
            {
                // spawn hit entity in current direction
                Entity hit = new Entity()
                {
                    Position = Entity.Position,
                    Name = "hit"
                };
                hit["SPEED"] = 3;
                hit["DIRECTION"] = Entity["DIRECTION"];
                hit.Features.Add(new MoveFeature());
                hit.Features.Add(new DieOnCollisonFeature());
                hit.Features.Add(new DieAfterTimeFeature() {
                    TimeTillDeath = 0.5f
                });
                hit.Features.Add(new HitFeature(this.collisionSystem) {
                    HitBox = new Rectangle(-5, -5, 10, 10),
                    Damage = 10,
                    SpawnedBy = Entity
                });
                hit.ProcessMessage(new StartMovingMessage());

                Entity.ProcessMessage(new SpawnMessage()
                {
                    EntityToSpawn = hit
                });
            }
        }
    }
}
