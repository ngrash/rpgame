using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RPGame.Messaging.Messages;

namespace RPGame.Features
{
    class CollidableFeature : Feature
    {
        CollisionSystem collisionSystem;

        public Rectangle HitBox
        {
            get;
            set;
        }

        public CollidableFeature(CollisionSystem collisionSystem)
        {
            this.collisionSystem = collisionSystem;
        }

        public IEnumerable<Entity> GetCollidingEntities()
        {
            return new Entity[0];
        }

        public void TryMove(Point desiredPosition)
        {
            Point nearestPossiblePosition;
            if (!this.collisionSystem.IsMovementLegal(this, desiredPosition, out nearestPossiblePosition))
            {
                Entity.Position = nearestPossiblePosition;
                Entity.ProcessMessage(new StopMovingMessage());
            }
            else
            {
                Entity.Position = desiredPosition;
            }
        }
    }
}
