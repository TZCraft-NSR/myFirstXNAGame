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
        protected Keys up;
        protected Keys down;
        public Color color;

        protected myFirstXNAGame myGame;

        public PaddleRight(Texture2D texture, Vector2 position, Color color, myFirstXNAGame myGame, Keys up, Keys down) : base(position)
        {
            AddAnimations(texture);

            if (up == Keys.None && up == Keys.None)
            {
                ai = true;
            }
            else
            {
                ai = false;
            }

            speed = 3;
            direction = Vector2.Zero;

            this.up = up;
            this.down = down;
            this.position = position;
            this.myGame = myGame;
            this.color = color;
        }

        public override void Update(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && !dummy && move == false && myGame.ableStart == true)
            {
                move = true;
                myGame.isStarted = true;
            }

            if (ai == false && move == true && !dummy && !myGame.gameOver)
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

            if (ai == true && move == true && !dummy && !myGame.gameOver)
            {
                if (myGame.pongposition.Y + 20 < position.Y + 50)
                {
                    position.Y -= speed;
                }
                if (myGame.pongposition.Y + 20 > position.Y + 50)
                {
                    position.Y += speed;
                }
            }

            if (dummy)
            {
                if (myGame.dummyPongposition.Y + 20 < position.Y + 50)
                {
                    position.Y -= speed;
                }
                if (myGame.dummyPongposition.Y + 20 > position.Y + 50)
                {
                    position.Y += speed;
                }
            }

            //position += direction * speed;

            if (position.Y < 100)
            {
                position.Y = 100;
            }
            if (position.Y > myGame.windowSize.Y - currentAnimation.frameSize.Y)
            {
                position.Y = myGame.windowSize.Y - currentAnimation.frameSize.Y;
            }

            myGame.PaddleSegments[3] = new Collision.MapSegment(new Point((int)position.X, (int)position.Y), new Point((int)position.X, (int)position.Y + currentAnimation.frameSize.Y));
            myGame.PaddleSegments[4] = new Collision.MapSegment(new Point((int)position.X, (int)position.Y), new Point((int)position.X + currentAnimation.frameSize.X, (int)position.Y));
            myGame.PaddleSegments[5] = new Collision.MapSegment(new Point((int)position.X, (int)position.Y + currentAnimation.frameSize.Y), new Point((int)position.X + currentAnimation.frameSize.X, (int)position.Y + currentAnimation.frameSize.Y));

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnimation.texture, position, new Rectangle(currentAnimation.startPos.X + (currentFrame.X * currentAnimation.frameSize.X), currentAnimation.startPos.Y + (currentFrame.Y * currentAnimation.frameSize.Y), currentAnimation.frameSize.X, currentAnimation.frameSize.Y), color);
        }

        public override void AddAnimations(Texture2D texture)
        {
            Addanimation("IDLE", texture, new Point(50, 100), new Point(0, 0), new Point(0, 0), 1000);
            SetAnimation("IDLE");
            base.AddAnimations(texture);
        }
    }
}