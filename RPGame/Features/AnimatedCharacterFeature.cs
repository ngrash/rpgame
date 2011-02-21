using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGame.Messaging;
using RPGame.Messaging.Messages;
using RPGame.Tiles;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;

namespace RPGame.Features
{
    public class AnimatedCharacterFeature : SpriteFeature, IMessageReceiver
    {
        float lastFrame = 0;

        public Direction Direction
        {
            get;
            set;
        }

        public CharacterTileset.CharacterPoseType Pose
        {
            get;
            set;
        }

        public CharacterTileset Tileset
        {
            get;
            set;
        }

        public override void Update(float timeElapsed)
        {
            this.lastFrame += timeElapsed;

            if (lastFrame >= 0.15)
            {
                if (Pose == CharacterTileset.CharacterPoseType.Walking1)
                {
                    Pose = CharacterTileset.CharacterPoseType.Walking2;
                }
                else if (Pose == CharacterTileset.CharacterPoseType.Walking2)
                {
                    Pose = CharacterTileset.CharacterPoseType.Walking1;
                }

                this.lastFrame = 0;
            }

            Surface charSurface = Tileset.GetPose(Pose, Direction);
            this.Sprite = new Sprite(charSurface);
        }

        public void ReceiveMessage(IMessage message)
        {
            if (message is StartMovingMessage)
            {
                Direction = (Direction)Entity["DIRECTION"];
                Pose = CharacterTileset.CharacterPoseType.Walking1;
            }
            else if (message is StopMovingMessage)
            {
                Pose = CharacterTileset.CharacterPoseType.Standing;
            }
        }
    }
}
