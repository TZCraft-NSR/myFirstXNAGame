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
        protected bool moveAi;
        protected Keys up;
        protected Keys down;

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

            this.myGame = myGame;

            direction = Vector2.Zero;
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
            else
            {
                if (keyboardState.IsKeyDown(Keys.Space) && moveAi == false)
                {
                    moveAi = true;
                }

                if (moveAi == true)
                {
                    if (myGame.pongPosition.Y + 20 < position.Y + 50)
                    {
                        direction.Y = -1;
                    }
                    else if (myGame.pongPosition.Y + 20 > position.Y + 50)
                    {
                        direction.Y = 1;
                    }
                    else
                    {
                        position.Y = 0;
                    }
                }

                position += direction * speed;
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
