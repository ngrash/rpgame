using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;
using SdlDotNet.Core;

namespace RPGame
{
    class InputSystem
    {
        Dictionary<string, IInputReceiver> inputReceivers = new Dictionary<string, IInputReceiver>();
        IInputReceiver activeInputReceiver;

        public InputSystem()
        {
            Events.KeyboardDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(Events_KeyboardDown);
            Events.KeyboardUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(Events_KeyboardUp);
        }

        public void AddChannel(IInputReceiver channel, string name)
        {
            this.inputReceivers.Add(name, channel);
        }

        public void ActivateChannel(string name)
        {
            this.activeInputReceiver = this.inputReceivers[name];
        }

        void Events_KeyboardUp(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (this.activeInputReceiver != null)
            {
                this.activeInputReceiver.HandleInput(new UserInputMessage() { KeyboardEvent = e });
            }
        }

        void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (e.Key == SdlDotNet.Input.Key.Escape)
            {
                Events.QuitApplication();
            }

            if(this.activeInputReceiver != null) {
            this.activeInputReceiver.HandleInput(new UserInputMessage() { KeyboardEvent = e });
                }
        }
    }
}
