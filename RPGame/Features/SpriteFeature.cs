﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics.Sprites;
using System.Drawing;

namespace RPGame.Features
{
    public class SpriteFeature : Feature
    {
        public Sprite Sprite
        {
            get;
            set;
        }

        public Point Offset
        {
            get;
            set;
        }
    }
}
