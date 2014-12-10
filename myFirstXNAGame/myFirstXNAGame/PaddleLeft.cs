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
        protected Keys up;
        protected Keys down;

        protected myFirstXNAGame myGame;

        public PaddleLeft(Texture2D texture, Vector2 position, myFirstXNAGame myGame, Keys up, Keys down) : base(position)
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

            speed = 3;

            this.up = up;
            this.down = down;

            this.position = position;
            this.myGame = myGame;

            direction.X = (0);
            direction.Y = (0);
        }

        public override void Update(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && move == false && myGame.ableStart == true)
            {
                move = true;
            }

            if (move == true && myGame.ableStart == true)
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

            myGame.PaddleSegments[0] = new Collision.MapSegment(new Point((int)position.X + currentAnimation.frameSize.X, (int)position.Y), new Point((int)position.X + currentAnimation.frameSize.X, (int)position.Y + currentAnimation.frameSize.Y));
            myGame.PaddleSegments[1] = new Collision.MapSegment(new Point((int)position.X, (int)position.Y), new Point((int)position.X + currentAnimation.frameSize.X, (int)position.Y));
            myGame.PaddleSegments[2] = new Collision.MapSegment(new Point((int)position.X, (int)position.Y + currentAnimation.frameSize.Y), new Point((int)position.X + currentAnimation.frameSize.X, (int)position.Y + currentAnimation.frameSize.Y));

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
