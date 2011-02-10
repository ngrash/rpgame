using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Features
{
    class DestructibleFeature : Feature
    {
        public int Health
        {
            get { return (int)Entity["HEALTH"]; }
            set { Entity["HEALTH"] = value; }
        }

        public int MaxHealt
        {
            get { return (int)Entity["MAX_HEALTH"]; }
            set { Entity["MAX_HEALT"] = value; }
        }

        public override void Update(float timeElapsed)
        {
            
        }
    }
}
