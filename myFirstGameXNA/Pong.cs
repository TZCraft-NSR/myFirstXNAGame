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
    /// <summary>
    /// The Pong class
    /// </summary>
    class Pong : Sprite
    {
        myFirstXNAGame myGame;
        public Color color;
        /// <summary>
        /// Creates the Pong from the class.
        /// </summary>
        /// <param name="position">The position of the pong</param>
        public Pong(Texture2D texture, Vector2 position, Color color, myFirstXNAGame myGame) : base(position)
        {
            AddAnimations(texture);
            speed = 3;
            this.myGame = myGame;
            this.color = color;

            direction.X = (2 * (float)myGame.rng.NextDouble() - 1);
            direction.Y = (2 * (float)myGame.rng.NextDouble() - 1);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && myGame.menu == 1 && move == false && myGame.ableStart == true)
            {
                move = true;
            }

            if (move == true)
            {
                position.X += direction.X * speed;
                position.Y += direction.Y * speed;

                foreach (Collision.MapSegment ms in myGame.MapSegments)
                {
                    if (collisionRect().Intersects(ms.collisionRect()))
                    {
                        direction = Collision.reflectedVector(direction, ms.getVector());
                        speed *= 1.01f;

                        if (speed > 30)
                        {
                            speed = 25;
                        }

                        if (!dummy)
                        {
                            if (collisionRect().Intersects(myGame.MapSegments[4].collisionRect()))
                            {
                                speed = 0;
                                myGame.gameOver = true;
                                myGame.playerWon = false;
                                move = false;
                            }
                            if (collisionRect().Intersects(myGame.MapSegments[5].collisionRect()))
                            {
                                speed = 0;
                                myGame.gameOver = true;
                                myGame.playerWon = true;
                                move = false;
                            }
                        }

                        if (dummy)
                        {
                            if (collisionRect().Intersects(myGame.MapSegments[4].collisionRect()))
                            {
                                position = myGame.pongDefaultposition;

                                direction.X = (2 * (float)myGame.rng.NextDouble() - 1);
                                direction.Y = (2 * (float)myGame.rng.NextDouble() - 1);

                                speed = 3;
                            }
                            if (collisionRect().Intersects(myGame.MapSegments[5].collisionRect()))
                            {
                                position = myGame.pongDefaultposition;

                                direction.X = (2 * (float)myGame.rng.NextDouble() - 1);
                                direction.Y = (2 * (float)myGame.rng.NextDouble() - 1);

                                speed = 3;
                            }
                        }

                        position += direction * speed;
                    }
                }

                foreach (Collision.MapSegment ms in myGame.PaddleSegments)
                {
                    if (collisionRect().Intersects(ms.collisionRect()))
                    {
                        direction = Collision.reflectedVector(direction, ms.getVector());
                        speed *= 1.01f;

                        if (speed > 30)
                        {
                            speed = 25;
                        }

                        if (!dummy && !myGame.gameOver)
                        {
                            if (collisionRect().Intersects(myGame.PaddleSegments[0].collisionRect()))
                            {
                                myGame.scoreLeft += 1;
                            }
                            if (collisionRect().Intersects(myGame.PaddleSegments[3].collisionRect()))
                            {
                                myGame.scoreRight += 1;
                            }
                        }

                        position += direction * speed;
                    }
                }
            }

            if (!dummy)
            {
                myGame.pongposition = position;
            }
            else
            {
                myGame.dummyPongposition = position;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnimation.texture, position, new Rectangle(currentAnimation.startPos.X + (currentFrame.X * currentAnimation.frameSize.X), currentAnimation.startPos.Y + (currentFrame.Y * currentAnimation.frameSize.Y), currentAnimation.frameSize.X, currentAnimation.frameSize.Y), color);
        }

        public override void AddAnimations(Texture2D texture)
        {
            Addanimation("IDLE", texture, new Point(40, 40), new Point(0, 0), new Point(0, 0), 1000);
            SetAnimation("IDLE");
            base.AddAnimations(texture);
        }
    }
}
