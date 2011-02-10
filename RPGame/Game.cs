using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System.Drawing;
using RPGame.Tiles;
using RPGame.Messaging.Messages;
using System.IO;

namespace RPGame
{
    class Game
    {
        Surface screenSurface;
        List<Entity> enties = new List<Entity>();
        RenderSystem renderSystem;
        CameraSystem cameraSystem;
        Map map = new Map();

        static void Main(string[] args)
        {
            new Game().Run();
        }

        void Run()
        {
            Events.Quit += (s, e) => Events.QuitApplication();
            Events.Tick += new EventHandler<TickEventArgs>(Events_Tick);
            Events.KeyboardDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(Events_KeyboardDown);
            Events.KeyboardUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(Events_KeyboardUp);

            this.screenSurface = Video.SetVideoMode(300, 300, false);
            this.renderSystem = new RenderSystem(this.screenSurface, this.map);
            this.cameraSystem = new CameraSystem();

            this.map.MessageIncoming += this.cameraSystem.ReceiveMessage;
            this.map.Load();

            Events.Run();
        }

        void Events_KeyboardUp(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            this.map.ProcessMessage(new UserInputMessage() { KeyboardEvent = e });
        }

        void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            this.map.ProcessMessage(new UserInputMessage() { KeyboardEvent = e });
        }

        void Events_Tick(object sender, TickEventArgs e)
        {
            this.cameraSystem.Update(e.SecondsElapsed);
            this.map.Update(e.SecondsElapsed);

            Point cameraPosition = this.cameraSystem.CameraPosition;
            this.renderSystem.Update(cameraPosition);
        }
    }
}
