using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using VoidEngine;

namespace myFirstXNAGame
{
    public class PaddleRight : Sprite
    {
        public PaddleRight(Texture2D tex, Vector2 pos, Keys u, Keys d) : base(tex, pos)
        {
            texture = tex;
            position = pos;

            frameSize = new Point(60, 50);
            sheetSize = new Point(5, 6);
            fTime = 16;

            up = u;
            down = d;
        }

        public PaddleRight(Texture2D tex, Vector2 pos, int frameWidth, int frameHeight, int sheetWidth, int sheetHeight, int fps, Keys u, Keys d) : base(tex, pos, frameWidth, frameHeight, sheetWidth, sheetHeight, fps)
        {
            texture = tex;
            position = pos;

            frameSize = new Point(frameWidth, frameHeight);
            sheetSize = new Point(sheetWidth, sheetHeight);
            fTime = fps;

            up = u;
            down = d;
        }

        public override void Update(GameTime gameTime)
        {
            if (up != Keys.None && down != Keys.None)
            {
                if (keyboardState.IsKeyDown(up))
                {
                    position.Y -= 1;
                }
                if (keyboardState.IsKeyDown(down))
                {
                    position.Y += 1;
                }

                if (position.Y <= 0)
                {
                    position.Y = 0;
                }
                if (position.Y >= 480 - frameSize.Y)
                {
                    position.Y = 480 - frameSize.Y;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
