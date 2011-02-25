using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Input;

namespace RPGame
{
    class DirectionHelper
    {
        public static Direction GetDirectionFromKey(Key key)
        {
            Direction direction = Direction.None;

            switch (key)
            {
                case Key.UpArrow:
                    direction = Direction.Up;
                    break;
                case Key.DownArrow:
                    direction = Direction.Down;
                    break;
                case Key.LeftArrow:
                    direction = Direction.Left;
                    break;
                case Key.RightArrow:
                    direction = Direction.Right;
                    break;
            }

            return direction;
        }
    }
}
