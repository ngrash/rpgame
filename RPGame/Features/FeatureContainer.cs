using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;

namespace RPGame.Features
{
    public class FeatureContainer
    {
        Entity associatedEntity;
        List<IFeature> features = new List<IFeature>();

        public FeatureContainer(Entity associatedEntity)
        {
            if (associatedEntity == null)
            {
                throw new ArgumentNullException("associatedEntity");
            }

            this.associatedEntity = associatedEntity;
        }

        public void Add(IFeature feature)
        {
            if (!this.features.Contains(feature))
            {
                this.features.Add(feature);
                feature.Entity = this.associatedEntity;
                
                if (feature is IMessageReceiver)
                {
                    this.associatedEntity.MessageIncoming += ((IMessageReceiver)feature).ReceiveMessage;
                }

                feature.Initialize();
            }
        }

        public TFeature Get<TFeature>() where TFeature : IFeature
        {
            foreach (IFeature feature in this.features)
            {
                if (typeof(TFeature).IsAssignableFrom(feature.GetType()))
                {
                    return (TFeature)feature;
                }
            }

            return default(TFeature);
        }

        public void Remove(IFeature feature)
        {
            this.features.Remove(feature);
            feature.Entity = null;

            if (feature is IMessageReceiver)
            {
                this.associatedEntity.MessageIncoming -= ((IMessageReceiver)feature).ReceiveMessage;
            }
        }

        public void Update(float timeElapsed)
        {
            foreach (IFeature feature in this.features)
            {
                feature.Update(timeElapsed);
            }
        }
    }
}
