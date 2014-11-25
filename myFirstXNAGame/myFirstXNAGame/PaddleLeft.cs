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
    public class PaddleLeft : Sprite
    {
        protected bool move;
        protected int speed;
        protected Keys up;
        protected Keys down;
        protected Keys left;
        protected Keys right;

        public PaddleLeft(Vector2 position, Keys up, Keys down) : base(position)
        {
            move = true;

            this.up = up;
            this.down = down;
        }

        public override void Update(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(up))
            {
                position.Y -= 2;
            }
            if (keyboardState.IsKeyDown(down))
            {
                position.Y += 2;
            }

            if (position.Y <= 0)
            {
                position.Y = 0;
            }
            if (position.Y >= 480 - currentAnimation.frameSize.Y)
            {
                position.Y = 480 - currentAnimation.frameSize.Y;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
