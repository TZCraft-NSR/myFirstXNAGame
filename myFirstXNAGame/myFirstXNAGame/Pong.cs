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
        Vector2 direction;

        /// <summary>
        /// Creates the Pong from the class.
        /// </summary>
        /// <param name="position">The position of the pong</param>
        public Pong(Texture2D texture, Vector2 position, myFirstXNAGame myGame) : base(position)
        {
            AddAnimations(texture);
            speed = 2;
            this.position = position;
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
            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }

            direction = Collision.UnitVector(direction);

            direction.X = direction.X * speed;
            direction.Y = direction.Y * speed;

            foreach (Collision.MapSegment ms in myGame.MapSegments)
            {
                if (collisionRect().Intersects(ms.collisionRect()))
                {
                    speed = 0;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Put inbetween the spriteBatch.Begin and spriteBatch.End
        /// </summary>
        /// <param name="gameTime">The main GameTime</param>
        /// <param name="spriteBatch">The main SpriteBatch</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void AddAnimations(Texture2D texture)
        {
            Addanimation("IDLE", texture, new Point(40, 40), new Point(0, 0), new Point(0, 0), 1000);
            SetAnimation("IDLE");
            base.AddAnimations(texture);
        }
    }
}
