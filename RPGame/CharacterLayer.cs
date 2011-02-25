using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;
using RPGame.Messaging;
using RPGame.Messaging.Messages;
using SdlDotNet.Graphics.Primitives;
using SdlDotNet.Input;

namespace RPGame
{
    class CharacterLayer : Layer, IInputReceiver
    {
        Entity player;
        Point cursorPosition = new Point(0, 0);
        Rectangle[][] panels;
        Surface background;
        SdlDotNet.Graphics.Font font = new SdlDotNet.Graphics.Font(@"I:\Remote\Desktop\rpgame\RPGame\bin\Debug\arial.ttf", 12);

        public bool Active
        {
            get;
            set;
        }

        public CharacterLayer(Entity player)
        {
            this.player = player;

            this.panels = new Rectangle[3][];
            this.panels[0] = new Rectangle[2];
            this.panels[0][0] = new Rectangle(104, 16, 15, 15);
            this.panels[0][1] = new Rectangle(121, 16, 15, 15);
            this.panels[1] = new Rectangle[2];
            this.panels[1][0] = new Rectangle(104, 39, 15, 15);
            this.panels[1][1] = new Rectangle(121, 39, 15, 15);
            this.panels[2] = new Rectangle[2];
            this.panels[2][0] = new Rectangle(104, 62, 15, 15);
            this.panels[2][1] = new Rectangle(121, 62, 15, 15);

            this.background = new Surface(new Bitmap(@"I:\Remote\Desktop\rpgame\RPGame\bin\Debug\char.PNG"));
        }

        public override Surface GetViewSpace(Point camera, Size viewSize)
        {
            Surface surface = new Surface(viewSize)
            {
                SourceColorKey = Color.Pink
            };
            surface.Fill(Color.Pink);

            if (Active)
            {
                surface.Blit(this.background);

                for (int row = 0; row < this.panels.Length; row++)
                {
                    Rectangle[] panelsInRow = this.panels[row];

                    for (int column = 0; column < panelsInRow.Length; column++)
                    {
                        Rectangle panel = panelsInRow[column];

                        if (row == this.cursorPosition.Y && column == this.cursorPosition.X)
                        {
                            surface.Draw(new Box(panel.Location, panel.Size), Color.Gold);
                        }                        
                    }
                }

                string textStrength = string.Format("Strength: {0}", this.player.Attributes.Get<int>("STRENGTH"));
                Surface strengthSurface = this.font.Render(textStrength, Color.Gold, true);
                surface.Blit(strengthSurface, new Point(12, 16));

                string textDexterity = string.Format("Dexterity: {0}", this.player.Attributes.Get<int>("DEXTERITY"));
                Surface dexteritySurface = this.font.Render(textDexterity, Color.Gold, true);
                surface.Blit(dexteritySurface, new Point(12, 39));

                string textStamina = string.Format("Stamina: {0}", this.player.Attributes.Get<int>("STAMINA"));
                Surface staminaSurface = this.font.Render(textStamina, Color.Gold, true);
                surface.Blit(staminaSurface, new Point(12, 62));
            }

            return surface;
        }

        public void HandleInput(UserInputMessage userInputMessage)
        {
            Direction direction = DirectionHelper.GetDirectionFromKey(userInputMessage.KeyboardEvent.Key);
            if (direction != Direction.None && userInputMessage.KeyboardEvent.Down)
            {
                int x = this.cursorPosition.X;
                int y = this.cursorPosition.Y;

                switch (direction)
                {
                    case Direction.Down:
                        y++;
                        if (y > this.panels[x].Length) y = 0;
                        break;
                    case Direction.Left:
                        x--;
                        if (x < 0) x = this.panels[y].Length - 1;
                        break;
                    case Direction.Right:
                        x++;
                        if (x >= this.panels[y].Length) x = 0;
                        break;
                    case Direction.Up:
                        y--;
                        if (y < 0) y = this.panels[x].Length;
                        break;
                }

                this.cursorPosition = new Point(x, y);
            }
            else
            {
                if (userInputMessage.KeyboardEvent.Down && userInputMessage.KeyboardEvent.Key == Key.C)
                {
                    Active = false;
                    InputSystem.ActivateChannel("map");
                }
            }
        }

        public InputSystem InputSystem
        {
            get;
            set;
        }
    }
}
