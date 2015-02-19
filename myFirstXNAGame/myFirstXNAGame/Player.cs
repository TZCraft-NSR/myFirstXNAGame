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
    public class Player : Sprite
    {
        protected Keys up;
        protected Keys down;
        protected Keys left;
        protected Keys right;
        protected Keys attack;
        protected Keys shoot;
        protected Vector2 vi = new Vector2(0, 0);
        protected bool IsJumping;
        protected readonly Vector2 g = new Vector2(0, -9.8f);

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
                    IsJumping = true;
                }
                if (keyboardState.IsKeyDown(left))
                {
                    position.X -= speed;
                }
                if (keyboardState.IsKeyDown(down))
                {
                    IsJumping = false;
                    //position.Y += speed;
                }
                if (keyboardState.IsKeyDown(right))
                {
                    position.X += speed;
                }

                if (position.Y <= 0)
                {
                    position.Y = 0;
                }
                if (position.Y >= 480)
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

                if (IsJumping == true)
                {
                    if (position.Y < 10)
                    {
                        vi.Y += g.Y; //* gameTime.ElapsedGameTime.Milliseconds;
                        position.Y += (float)vi.Y; //* gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else if (position.Y >= 10)
                    {
                        IsJumping = false;
                        vi.Y -= g.Y; //* gameTime.ElapsedGameTime.Milliseconds;
                        position.Y -= (float)vi.Y; //* gameTime.ElapsedGameTime.Milliseconds;
                    }
                }
                else if (IsJumping == false)
                {
                    //if (position.Y > 0)
                    //{
                    //    vi.Y -= g.Y; //* gameTime.ElapsedGameTime.Milliseconds;
                    //    position.Y -= (float)vi.Y; //* gameTime.ElapsedGameTime.Milliseconds;
                    //}
                    if (position.Y >= 430)
                    {
                        vi.Y = 0;
                    }

                    if (position.Y > 480)
                    {
                        position.Y = 0;
                    }
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

        public void addAnimations(List<String> animationName, List<Texture2D> textureList, List<Point> textureSize, List<Point> sheetCords, List<Point> frameCords, List<Int32> fps, string defaultAnimation)
        {
            foreach (String aN in animationName)
            {
                foreach (Texture2D tL in textureList)
                {
                    foreach (Point tS in textureSize)
                    {
                        foreach (Point sC in sheetCords)
                        {
                            foreach (Point fC in frameCords)
                            {
                                foreach (Int32 f in fps)
                                {
                                    Addanimation(aN, tL, tS, sC, fC, f);
                                }
                            }
                        }
                    }
                }
            }

            SetAnimation(defaultAnimation);
        }
    }
}