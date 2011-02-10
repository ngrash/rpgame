using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Tiles;
using System.Drawing;
using SdlDotNet.Graphics;
using System.Xml.Serialization;

namespace RPGame
{
    public class TileLayer : Layer
    {
        const int TILE_SIZE = 32;
        const int TILES_DIMENSION_X = 0;
        const int TILES_DIMENSION_Y = 1;

        Tile[,] tiles;

        public TileLayer()
        {
            this.tiles = new Tile[5, 5];

            Bitmap rawTileset = new Bitmap(@"X:\Develop\Projekte\RPGame\RPGame\bin\Debug\cell.png");
            Tileset tileset = new Tileset(rawTileset, TILE_SIZE);

            for (int x = 0; x < this.tiles.GetLength(TILES_DIMENSION_X); x++)
            {
                for (int y = 0; y < this.tiles.GetLength(TILES_DIMENSION_Y); y++)
                {
                    this.tiles[x, y] = new Tile(tileset, 0, 0);
                }
            }
        }

        public override Surface GetViewSpace(Point camera, Size viewSize)
        {
            Surface viewSpace = new Surface(viewSize);

            Point offset = new Point(camera.X - (viewSize.Width / 2), camera.Y - (viewSize.Height / 2));

            int tileX = offset.X / TILE_SIZE;
            int xStart = (offset.X % TILE_SIZE) * -1;
            int xEnd = xStart + viewSize.Width + (xStart == 0 ? 0 : TILE_SIZE);

            int tileY = offset.Y / TILE_SIZE;
            int yStart = (offset.Y % TILE_SIZE) * -1;
            int yEnd = yStart + viewSize.Height + (yStart == 0 ? 0 : TILE_SIZE);

            for (int y = yStart; y < yEnd; y += TILE_SIZE)
            {
                tileX = offset.X / TILE_SIZE;

                for (int x = xStart; x < xEnd; x += TILE_SIZE)
                {
                    if (tileX >= 0 && tileY >= 0 && tileX < this.tiles.GetLength(TILES_DIMENSION_X) && tileY < this.tiles.GetLength(TILES_DIMENSION_Y))
                    {
                        Tile tile = this.tiles[tileX, tileY];
                        if (tile != null)
                        {
                            viewSpace.Blit(tile.GetSurface(), new Point(x, y));
                        }
                    }

                    tileX++;
                }

                tileY++;
            }

            return viewSpace;
        }
    }
}
