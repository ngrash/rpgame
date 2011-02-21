using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RPGame.Features
{
    class HitFeature : Feature
    {
        CollisionSystem collisionSystem;

        public int Damage
        {
            get;
            set;
        }

        public Rectangle HitBox
        {
            get;
            set;
        }

        public Entity SpawnedBy
        {
            get;
            set;
        }

        public HitFeature(CollisionSystem collisionSystem)
        {
            this.collisionSystem = collisionSystem;
        }

        public override void Update(float timeElapsed)
        {
            this.collisionSystem.AddHitBox(Entity, HitBox);
        }
    }
}
