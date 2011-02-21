using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RPGame.Messaging.Messages;
using RPGame.Messaging;

namespace RPGame.Features
{
    class DestructibleFeature : Feature, IMessageReceiver
    {
        CollisionSystem collisionSystem;

        public int Health
        {
            get;
            set;
        }

        public int MaxHealt
        {
            get;
            set;
        }

        public DestructibleFeature(CollisionSystem collisionSystem)
        {
            this.collisionSystem = collisionSystem;
        }

        public override void Update(float timeElapsed)
        {
            this.collisionSystem.AddHitBox(Entity, HitBox);

            if (Health <= 0)
            {
                Entity.ProcessMessage(new KillMessage()
                {
                    EntityToKill = Entity
                });
            }
        }

        public Rectangle HitBox
        {
            get;
            set;
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is CollisionMessage)
            {
                Entity entity = ((CollisionMessage)message).WithEntity;
                HitFeature entityHitFeature = entity.Features.Get<HitFeature>();
                if (entityHitFeature != null)
                {
                    Health -= entityHitFeature.Damage;

                    ExperienceFeature experienceFeature = Entity.Features.Get<ExperienceFeature>();
                    if (experienceFeature != null)
                    {
                        if (!experienceFeature.Entities.Contains(entityHitFeature.SpawnedBy))
                        {
                            experienceFeature.Entities.Add(entityHitFeature.SpawnedBy);
                        }
                    }
                }
            }
        }
    }
}
