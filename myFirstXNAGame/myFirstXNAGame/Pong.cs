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
    class Pong : Sprite
    {
        protected bool start = true;

        public Pong(Vector2 position) : base(position)
        {
        }

        public override void Update(GameTime gameTime, Vector2 left, Vector2 right, Vector2 leftSize, Vector2 rightSize)
        {
            if (start == true)
            {
                position.X += 2;
                start = false;
            }

            if (position.Y <= left.Y && position.Y >= leftSize.Y)
            {
                if (position.X <= left.X + leftSize.X)
                {
                    position.X += 2;
                }
            }
            if (position.Y >= right.Y && position.Y >= rightSize.Y)
            {
                if (position.X <= right.X + rightSize.X)
                {
                    position.X -= 2;
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
