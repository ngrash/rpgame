using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace RPGame
{
    public abstract class Layer
    {
        public abstract Surface GetViewSpace(Point camera, Size viewSize);
    }
}
