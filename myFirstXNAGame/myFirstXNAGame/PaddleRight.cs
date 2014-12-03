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
        protected bool move;
        protected Keys up;
        protected Keys down;

        protected Vector2 direction;
        protected myFirstXNAGame myGame;

        public PaddleRight(Texture2D texture, Vector2 position, myFirstXNAGame myGame, Keys up, Keys down) : base(position)
        {
            AddAnimations(texture);

            if (up == Keys.None && up == Keys.None)
            {
                move = false;
            }
            else
            {
                move = true;
            }

            speed = 2;

            this.up = up;
            this.down = down;

            this.position = position;
            this.myGame = myGame;

            direction.X = (0);
            direction.Y = (0);
        }

        public override void Update(GameTime gameTime)
        {
            if (move == true)
            {
                if (keyboardState.IsKeyDown(up))
                {
                    position.Y -= speed;
                }
                if (keyboardState.IsKeyDown(down))
                {
                    position.Y += speed;
                }
            }

            if (position.Y < 100)
            {
                position.Y = 100;
            }
            if (position.Y > myGame.windowSize.Y - currentAnimation.frameSize.Y)
            {
                position.Y = myGame.windowSize.Y - currentAnimation.frameSize.Y;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void AddAnimations(Texture2D texture)
        {
            Addanimation("IDLE", texture, new Point(50, 100), new Point(0, 0), new Point(0, 0), 1000);
            SetAnimation("IDLE");
            base.AddAnimations(texture);
        }
    }
}
