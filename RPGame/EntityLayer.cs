using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;
using RPGame.Features;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics.Primitives;

namespace RPGame
{
    public class EntityLayer : Layer
    {
        public List<Entity> Entities
        {
            get;
            set;
        }

        public EntityLayer()
        {
            Entities = new List<Entity>();
        }

        public override Surface GetViewSpace(Point camera, Size viewSize)
        {
            Surface viewSpace = new Surface(viewSize)
            {
                Transparent = true,
                TransparentColor = Color.Pink
            };

            viewSpace.Fill(Color.Pink);

            Point offset = new Point(camera.X - (viewSize.Width / 2), camera.Y - (viewSize.Height / 2));

            foreach (Entity entity in Entities.OrderBy(e => e.Position.Y))
            {
                Point entityScreenCoordinates = new Point((offset.X * -1) + entity.Position.X, (offset.Y * -1) + entity.Position.Y);

                // Sprite zeichnen, falls vorhanden
                SpriteFeature spriteFeature = entity.Features.Get<SpriteFeature>();
                if (spriteFeature != null)
                {
                    Sprite entitySprite = spriteFeature.Sprite;
                    Point spriteScreenCoordinates = new Point(entityScreenCoordinates.X + spriteFeature.Offset.X, entityScreenCoordinates.Y + spriteFeature.Offset.Y);

                    viewSpace.Blit(entitySprite, spriteScreenCoordinates);
                }

                // Kollisionsbox zeichen, falls das Entity kollidierbar ist
                CollidableFeature collidableFeature = entity.Features.Get<CollidableFeature>();
                if (collidableFeature != null)
                {
                    viewSpace.Draw(new Box(new Point(entityScreenCoordinates.X + collidableFeature.HitBox.X, entityScreenCoordinates.Y + collidableFeature.HitBox.Y), collidableFeature.HitBox.Size), Color.Green);
                }

                // Destructiblebox zeichen
                DestructibleFeature destructibleFeature = entity.Features.Get<DestructibleFeature>();
                if (destructibleFeature != null)
                {
                    viewSpace.Draw(new Box(new Point(entityScreenCoordinates.X + destructibleFeature.HitBox.X, entityScreenCoordinates.Y + destructibleFeature.HitBox.Y), destructibleFeature.HitBox.Size), Color.Blue);
                }

                // HitFeature HitBox zeichen
                HitFeature hitFeature = entity.Features.Get<HitFeature>();
                if (hitFeature != null)
                {
                    viewSpace.Draw(new Box(new Point(entityScreenCoordinates.X + hitFeature.HitBox.X, entityScreenCoordinates.Y + hitFeature.HitBox.Y), hitFeature.HitBox.Size), Color.Red);
                }

                // Eigentliche Position des Entitys hervorheben
                viewSpace.Draw(new Circle(entityScreenCoordinates, 3), Color.Yellow);
            }

            return viewSpace;
        }
    }
}
