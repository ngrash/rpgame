using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;

namespace RPGame.Features
{
    class LevelFeature : Feature, IMessageReceiver
    {
        const float FACTOR = 1.3f;

        public float Experience
        {
            get;
            set;
        }

        public float RequiredExperience
        {
            get;
            set;
        }

        public int Level
        {
            get;
            set;
        }

        public override void Update(float timeElapsed)
        {
            if (Experience >= RequiredExperience)
            {
                Experience -= RequiredExperience;
                RequiredExperience *= FACTOR;
                Level++;

                Entity.ProcessMessage(new LevelUpMessage()
                {
                    Target = Entity,
                    NewLevel = Level
                });
            }
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is ExperienceMessage)
            {
                float experience = ((ExperienceMessage)message).Experience;
                Experience += experience;
            }
        }
    }
}
