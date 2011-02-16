using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging.Messages;

namespace RPGame.Features
{
    class DieAfterTimeFeature : Feature
    {
        public float TimeTillDeath
        {
            get;
            set;
        }

        public override void Update(float timeElapsed)
        {
            TimeTillDeath -= timeElapsed;

            if (TimeTillDeath <= 0)
            {
                Entity.ProcessMessage(new KillMessage() 
                { 
                    EntityToKill = Entity 
                });
            }
        }
    }
}
