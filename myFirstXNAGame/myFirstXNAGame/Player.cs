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
    class Player : TwoAnimation
    {
        protected bool move;
        protected int speed;
        protected Keys up;
        protected Keys down;
        protected Keys left;
        protected Keys right;
        protected Keys attack;
        protected Keys shoot;

        public Player(Vector2 position, int speed, Keys up, Keys down, Keys left, Keys right, Keys attack, Keys shoot) : base(position)
        {
            move = true;

            this.position = position;

            this.speed = speed;
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            this.attack = attack;
            this.shoot = shoot;
        }

        public override void Update(GameTime gameTime)
        {
            if (move == true)
            {
                if (keyboardState.IsKeyDown(up))
                {
                    position.Y -= speed;
                }
                if (keyboardState.IsKeyDown(left))
                {
                    position.X -= speed;
                }
                if (keyboardState.IsKeyDown(down))
                {
                    position.Y += speed;
                }
                if (keyboardState.IsKeyDown(right))
                {
                    position.X += speed;
                }

                if (position.Y <= 0)
                {
                    position.Y = 0;
                }
                if (position.Y >= 480 - currentAnimation.frameSize.Y)
                {
                    position.Y = 480 - currentAnimation.frameSize.Y;
                }
                if (position.X <= 0)
                {
                    position.X = 0;
                }
                if (position.X >= 480 - currentAnimation.frameSize.X)
                {
                    position.X = 480 - currentAnimation.frameSize.X;
                }
            }

            if (keyboardState.IsKeyDown(left))
            {
                SetAnimation("WALK");
            }

            if (keyboardState.IsKeyDown(right))
            {
                SetAnimation("WALK");
            }

            if (keyboardState.IsKeyDown(up))
            {
                SetAnimation("JUMP");
            }

            if (keyboardState.IsKeyDown(down))
            {
                SetAnimation("BLOCK");
            }

            if (keyboardState.IsKeyDown(shoot))
            {
                SetAnimation("SHOOT");
            }

            if (keyboardState.IsKeyDown(attack))
            {
                SetAnimation("SWING");
            }

            if (!keyboardState.IsKeyDown(left) && !keyboardState.IsKeyDown(right) && !keyboardState.IsKeyDown(up) && !keyboardState.IsKeyDown(down) && !keyboardState.IsKeyDown(shoot) && !keyboardState.IsKeyDown(attack))
            {
                SetAnimation("IDLE");
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void AddAnimations(Texture2D texture)
        {
            Addanimation("IDLE", texture, new Point(60, 50), new Point(1, 1), new Point(0, 0), 40);
            Addanimation("WALK", texture, new Point(60, 50), new Point(4, 3), new Point(0, 0), 40);
            Addanimation("BLOCK", texture, new Point(60, 50), new Point(2, 2), new Point(180, 200), 100);
            Addanimation("SHOOT", texture, new Point(60, 50), new Point(1, 3), new Point(240, 0), 60);
            Addanimation("JUMP", texture, new Point(60, 50), new Point(5, 1), new Point(0, 150), 60);
            Addanimation("SWING", texture, new Point(60, 50), new Point(3, 2), new Point(0, 200), 70);
            SetAnimation("IDLE");
        }
    }
}
