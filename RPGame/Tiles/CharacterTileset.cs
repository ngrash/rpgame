using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace RPGame.Tiles
{
    public class CharacterTileset : Tileset
    {
        public enum CharacterPoseType
        {
            Walking1 = 0,
            Walking2 = 2,
            Standing = 1
        }

        public CharacterTileset(Bitmap rawTileset) : base(rawTileset, new Size(24, 32)) { }

        public Surface GetPose(CharacterPoseType pose, Direction direction)
        {
            return this.GetSurface((int)pose, (int)direction);
        }
    }
}
