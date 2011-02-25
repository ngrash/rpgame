using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace RPGame.Features
{
    class TextFeature : Feature
    {
        private string text;

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                SpriteFeature spriteFeature = Entity.Features.Get<SpriteFeature>();
                if (spriteFeature == null)
                {
                    throw new InvalidOperationException();
                }

                Surface textSurface = Font.Render(value, Color);
                spriteFeature.Sprite = new SdlDotNet.Graphics.Sprites.Sprite(textSurface);

                this.text = value;
            }
        }

        public SdlDotNet.Graphics.Font Font
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }
    }
}
