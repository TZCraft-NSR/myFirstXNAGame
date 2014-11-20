using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This is the myFirstXNAGame class
    /// </summary>
    public class myFirstXNAGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Sprite> spriteList;

        Texture2D texture;
        Texture2D texturePlayer;
        Texture2D texturePaddle;
        Texture2D texturePong;
        Random rng;

        public myFirstXNAGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            rng = new Random();
            // TODO: Add your initialization logic here
            spriteList = new List<Sprite>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>(@"Images\player");
            texturePlayer = Content.Load<Texture2D>(@"Images\john");
            texturePaddle = Content.Load<Texture2D>(@"Images\paddle");
            texturePong = Content.Load<Texture2D>(@"Images\pong");
            for (int i = 0; i < 1; i++)
            {
                //spriteList.Add(new OneAnimation(texture, new Vector2(rng.Next(600), rng.Next(600)), 60, 50, 5, 6, 16, 1, Keys.W, Keys.S, Keys.A, Keys.D));
                spriteList.Add(new OneAnimation(texture, new Vector2(rng.Next(600), rng.Next(600)), 60, 50, 5, 6, 16));
            }

            spriteList.Add(new PaddleLeft(texturePaddle, new Vector2(100, 100), 12, 50, 1, 1, 0, Keys.W, Keys.S));
            spriteList.Add(new PaddleRight(texturePaddle, new Vector2(300 - 24, 100), 12, 50, 1, 1, 0, Keys.None, Keys.None));

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            foreach (Sprite s in spriteList)
            {
                s.Update(gameTime);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(124, 198, 255));

            spriteBatch.Begin();
                foreach (Sprite s in spriteList)
                {
                    s.Draw(gameTime, spriteBatch);
                }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
