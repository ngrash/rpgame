using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RPGame.Messaging;
using RPGame.Messaging.Messages;

namespace RPGame
{
    class CameraSystem : IMessageReceiver
    {
        Entity focusedEntity;

        public Point CameraPosition
        {
            get;
            private set;
        }

        public void SetFocus(Entity focusedEntity)
        {
            this.focusedEntity = focusedEntity;
        }

        public void Update(float timeElapsed)
        {
            if (this.focusedEntity != null)
            {
                CameraPosition = this.focusedEntity.Position;
            }
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is SetFocusMessage)
            {
                SetFocus(((SetFocusMessage)message).TargetEntity);
            }
        }
    }
}
