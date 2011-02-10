using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;
using SdlDotNet.Core;

namespace RPGame
{
    class RenderSystem
    {
        Surface screenSurface;
        Map map;

        public RenderSystem(Surface screenSurface, Map map)
        {
            this.screenSurface = screenSurface;
            this.map = map;
        }

        public void Update(Point cameraOffset)
        {
            this.screenSurface.Blit(new Surface(map.GetViewSpace(cameraOffset, screenSurface.Size)));
            this.screenSurface.Update();
        }
    }
}
