using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Features;
using System.Drawing;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace RPGame
{
    public class Entity : IMessageChannel
    {
        public event Action<IMessage> MessageIncoming;

        //Dictionary<string, object> attributes = new Dictionary<string, object>();

        //public object this[string attributeName]
        //{
        //    get {
        //        attributeName = attributeName.ToUpper();

        //        object value;
        //        this.attributes.TryGetValue(attributeName, out value);
        //        return value;
        //    }
        //    set
        //    {
        //        attributeName = attributeName.ToUpper();

        //        if (!this.attributes.ContainsKey(attributeName))
        //        {
        //            this.attributes.Add(attributeName, value);
        //        }
        //        else
        //        {
        //            this.attributes[attributeName] = value;
        //        }
        //    }
        //}

        public AttributeCollection Attributes
        {
            get;
            set;
        }

        public Point Position
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public FeatureContainer Features
        {
            get;
            private set;
        }

        public void Move(Direction movingDirection, int steps)
        {
            Point newPosition = Position;

            switch (movingDirection)
            {
                case Direction.Down:
                    newPosition = new Point(Position.X, Position.Y + steps);
                    break;
                case Direction.Left:
                    newPosition = new Point(Position.X - steps, Position.Y);
                    break;
                case Direction.Right:
                    newPosition = new Point(Position.X + steps, Position.Y);
                    break;
                case Direction.Up:
                    newPosition = new Point(Position.X, Position.Y - steps);
                    break;
            }

            CollidableFeature collidableFeature = Features.Get<CollidableFeature>();
            if (collidableFeature != null)
            {
                collidableFeature.TryMove(newPosition);
            }
            else
            {
                Position = newPosition;
            }
        }

        public Entity()
        {
            Attributes = new AttributeCollection();
            Features = new FeatureContainer(associatedEntity: this);
        }

        public void ProcessMessage(IMessage message)
        {
            if (MessageIncoming != null)
            {
                MessageIncoming(message);
            }
        }

        public void Update(float timeElapsed)
        {
            Features.Update(timeElapsed);
        }
    }
}
