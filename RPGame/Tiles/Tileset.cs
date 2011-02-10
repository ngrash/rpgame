using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using SdlDotNet.Graphics;

namespace RPGame.Tiles
{
    public class Tileset
    {
        Surface[,] tileSurfaces;

        Bitmap RawTileset
        {
            get;
            set;
        }

        public Size TileSize
        {
            get;
            set;
        }

        public Tileset(Bitmap rawTileset, int tileSize)
            : this(rawTileset, new Size(tileSize, tileSize))
        { }

        public Tileset(Bitmap rawTileset, Size tileSize)
        {
            RawTileset = rawTileset;
            TileSize = tileSize;

            this.tileSurfaces = new Surface[rawTileset.Width / tileSize.Width, rawTileset.Height / tileSize.Height];
        }

        public Surface GetSurface(int x, int y)
        {
            if (this.tileSurfaces[x, y] == null)
            {
                int tileX = (TileSize.Width * x);
                int tileY = (TileSize.Height * y);
                Rectangle tileRect = new Rectangle(tileX, tileY, TileSize.Width, TileSize.Height);
                Bitmap tileBitmap = RawTileset.Clone(tileRect, PixelFormat.DontCare);
                Surface tileSurface = new Surface(tileBitmap);

                this.tileSurfaces[x, y] = tileSurface;
            }

            return this.tileSurfaces[x, y];
        }
    }
}
