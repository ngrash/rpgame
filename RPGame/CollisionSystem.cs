using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Features;
using System.Drawing;

namespace RPGame
{
    class CollisionSystem
    {
        List<Entity> entities = new List<Entity>();

        public bool IsMovementLegal(CollidableFeature collidableFeature, Point desiredPosition, out Point nearestPossiblePosition)
        {
            Rectangle entityHitBox = new Rectangle(new Point(desiredPosition.X + collidableFeature.HitBox.X, desiredPosition.Y + collidableFeature.HitBox.Y), collidableFeature.HitBox.Size);

            var collidables = entities.Select(e => e.Features.Get<CollidableFeature>()).Where(c => c != null);

            foreach (CollidableFeature collidable in collidables)
            {
                if (collidable != collidableFeature)
                {
                    Rectangle testEntityHitBox = new Rectangle(new Point(collidable.Entity.Position.X + collidable.HitBox.X, collidable.Entity.Position.Y + collidable.HitBox.Y), collidable.HitBox.Size);

                    if (entityHitBox.IntersectsWith(testEntityHitBox))
                    {
                        // TODO: hier eine bessere Position berechnen
                        nearestPossiblePosition = collidableFeature.Entity.Position;
                        return false;
                    }
                }
            }

            nearestPossiblePosition = Point.Empty;
            return true;
        }

        public void AddEntity(Entity entity)
        {
            if (!this.entities.Contains(entity))
            {
                this.entities.Add(entity);
            }
        }
    }
}
