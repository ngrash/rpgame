using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;
using RPGame.Features;
using SdlDotNet.Graphics.Primitives;

namespace RPGame
{
    class HudLayer : Layer
    {
        const int LEVEL_PANEL_WIDTH = 100;
        readonly Point LEVEL_PANEL_POSITION = new Point(2, 20);
        readonly Point HEALT_PANEL_POSITION = new Point(2, 40);
        const int PANEL_HEIGHT = 10;

        Entity player;

        public HudLayer(Entity player)
        {
            this.player = player;
        }

        public override Surface GetViewSpace(Point camera, Size viewSize)
        {
            Surface hudSurface = new Surface(viewSize)
            {
                SourceColorKey = Color.Pink
            };
            hudSurface.Fill(Color.Pink);

            LevelFeature levelFeature = this.player.Features.Get<LevelFeature>();
            if (levelFeature != null)
            {
                float levelPanelContentWidth = LEVEL_PANEL_WIDTH * (levelFeature.Experience / levelFeature.RequiredExperience);

                hudSurface.Fill(new Rectangle(LEVEL_PANEL_POSITION, new Size((int)levelPanelContentWidth, PANEL_HEIGHT)), Color.Yellow);
                hudSurface.Draw(new Box(LEVEL_PANEL_POSITION, new Size(LEVEL_PANEL_WIDTH, PANEL_HEIGHT)), Color.LightGoldenrodYellow);
            }

            DestructibleFeature destructibleFeature = this.player.Features.Get<DestructibleFeature>();
            if (destructibleFeature != null)
            {
                
            }

            return hudSurface;
        }
    }
}
