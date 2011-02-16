using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Features;
using System.Drawing;
using RPGame.Messaging.Messages;

namespace RPGame
{
    class CollisionSystem
    {
        List<Entity> entities = new List<Entity>();
        Dictionary<Entity, List<Rectangle>> entityToHitBox = new Dictionary<Entity, List<Rectangle>>();

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

        public void Update(float timeElapsed)
        {
            foreach (Entity entity1 in this.entityToHitBox.Keys)
            {
                List<Rectangle> hitBoxes = this.entityToHitBox[entity1];
                foreach (Rectangle hitbox1 in hitBoxes)
                {
                    foreach (Entity entity2 in this.entityToHitBox.Keys)
                    {
                        if (entity1 != entity2)
                        {
                            List<Rectangle> hitBoxes2 = this.entityToHitBox[entity2];
                            foreach (Rectangle hitbox2 in hitBoxes2)
                            {
                                Point hitBox1ToMap = new Point(entity1.Position.X + hitbox1.X, entity1.Position.Y + hitbox1.Y);
                                Point hitBox2ToMap = new Point(entity2.Position.X + hitbox2.X, entity2.Position.Y + hitbox2.Y);

                                Rectangle rect1 = new Rectangle(hitBox1ToMap, hitbox1.Size);
                                Rectangle rect2 = new Rectangle(hitBox2ToMap, hitbox2.Size);

                                if (rect1.IntersectsWith(rect2))
                                {
                                    entity1.ProcessMessage(new CollisionMessage()
                                    {
                                        WithEntity = entity2
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddHitBox(Entity entity, Rectangle hitBox)
        {
            List<Rectangle> hitBoxes;
            if (!this.entityToHitBox.TryGetValue(entity, out hitBoxes))
            {
                hitBoxes = new List<Rectangle>();
                this.entityToHitBox.Add(entity, hitBoxes);
            }

            if (!hitBoxes.Contains(hitBox))
            {
                hitBoxes.Add(hitBox);
            }
        }

        public void AddEntity(Entity entity)
        {
            if (!this.entities.Contains(entity))
            {
                this.entities.Add(entity);
            }
        }

        internal void RemoveEntity(Entity entity)
        {
            this.entities.Remove(entity);
            this.entityToHitBox.Remove(entity);
        }
    }
}
