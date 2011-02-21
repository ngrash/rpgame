using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;

namespace RPGame.Features
{
    class ExperienceFeature : Feature, IMessageReceiver
    {
        public List<Entity> Entities
        {
            get;
            set;
        }

        public float Experience
        {
            get;
            set;
        }

        public ExperienceFeature()
        {
            Entities = new List<Entity>();
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is KillMessage)
            {
                float experiencePerEntity = Experience / Entities.Count;
                foreach (Entity entity in Entities)
                {
                    entity.ProcessMessage(new ExperienceMessage()
                    {
                        Target = entity,
                        Experience = experiencePerEntity
                    });
                }
            }
        }
    }
}
