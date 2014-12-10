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

        /// <summary>
        /// Creates the Pong from the class.
        /// </summary>
        /// <param name="position">The position of the pong</param>
        public Pong(Texture2D texture, Vector2 position, myFirstXNAGame myGame) : base(position)
        {
            AddAnimations(texture);
            speed = 3;
            this.myGame = myGame;

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
            //direction = Collision.unitVector(direction);
            if (keyboardState.IsKeyDown(Keys.Space) && move == false && myGame.ableStart == true)
            {
                move = true;
            }

            if (move == true && myGame.ableStart == true)
            {
                position.X += direction.X * speed;
                position.Y += direction.Y * speed;

                foreach (Collision.MapSegment ms in myGame.MapSegments)
                {
                    if (collisionRect().Intersects(ms.collisionRect()))
                    {
                        //direction = Collision.reflectedVector(direction, ms.getVector());
                        direction = Collision.reflectedVector(direction, ms.getVector());
                        speed *= 1.01f;

                        if (speed > 30)
                        {
                            speed = 25;
                        }

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

                        position += direction * speed;
                    }
                }

                foreach (Collision.MapSegment ms in myGame.PaddleSegments)
                {
                    if (collisionRect().Intersects(ms.collisionRect()))
                    {
                        //direction = Collision.reflectedVector(direction, ms.getVector());
                        direction = Collision.reflectedVector(direction, ms.getVector());
                        speed *= 1.01f;

                        if (speed > 30)
                        {
                            speed = 25;
                        }

                        if (collisionRect().Intersects(myGame.PaddleSegments[0].collisionRect()) || collisionRect().Intersects(myGame.PaddleSegments[1].collisionRect()) || collisionRect().Intersects(myGame.PaddleSegments[2].collisionRect()))
                        {
                            myGame.scoreLeft += 1;
                        }
                        if (collisionRect().Intersects(myGame.PaddleSegments[3].collisionRect()) || collisionRect().Intersects(myGame.PaddleSegments[4].collisionRect()) || collisionRect().Intersects(myGame.PaddleSegments[5].collisionRect()))
                        {
                            myGame.scoreRight += 1;
                        }

                        position += direction * speed;


                    }
                }

                myGame.pongPosition = position;
            }

            base.Update(gameTime);
        }

        public override void AddAnimations(Texture2D texture)
        {
            Addanimation("IDLE", texture, new Point(40, 40), new Point(0, 0), new Point(0, 0), 1000);
            SetAnimation("IDLE");
            base.AddAnimations(texture);
        }
    }
}
