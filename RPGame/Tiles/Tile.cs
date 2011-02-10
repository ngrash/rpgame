using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace RPGame.Tiles
{
    class Tile
    {
        readonly int tileX;
        readonly int tileY;

        public bool Solid
        {
            get;
            set;
        }

        public Tileset Tileset
        {
            get;
            private set;
        }

        public Tile(Tileset tileset, int x, int y)
        {
            this.tileX = x;
            this.tileY = y;
            Tileset = tileset;
        }

        public Surface GetSurface()
        {
            return Tileset.GetSurface(tileX, tileY);
        }
    }
}
