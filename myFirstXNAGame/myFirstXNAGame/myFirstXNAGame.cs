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

        Sprite paddleLeft;
        Sprite paddleRight;
        Sprite pong;

        Texture2D texture;
        Texture2D texturePlayer;
        Texture2D texturePaddle;
        Texture2D texturePong;
        Texture2D bgTexture;
        Random rng;

        int screenWidth;
        int screenHeight;
        int calcEnd;
        int calcEnd2;
        int calcEnd3;
        int calcEnd4;

        public myFirstXNAGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
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
            bgTexture = Content.Load<Texture2D>(@"Images\background");
            for (int i = 0; i < 1; i++)
            {
                //spriteList.Add(new OneAnimation(texture, new Vector2(rng.Next(600), rng.Next(600)), 60, 50, 5, 6, 16, 1, Keys.W, Keys.S, Keys.A, Keys.D));
                spriteList.Add(new OneAnimation(texture, new Vector2(rng.Next(600), rng.Next(600)), 60, 50, 5, 6, 16));
            }

            screenWidth = graphics.PreferredBackBufferWidth;
            calcEnd = screenWidth - 75;
            calcEnd2 = (screenWidth - 40) / 2;
            calcEnd3 = (screenHeight * 2) + 40;
            calcEnd4 = (screenHeight + 100) / 2;

            paddleLeft = new PaddleLeft(texturePaddle, new Vector2(75, calcEnd4), 50, 100, 1, 1, 0, Keys.W, Keys.S);
            paddleRight = new PaddleRight(texturePaddle, new Vector2(calcEnd - 50, calcEnd4), 50, 100, 1, 1, 0, Keys.None, Keys.None);
            pong = new Pong(texturePong, new Vector2(calcEnd2, calcEnd3), 40, 40, 1, 1, 16);
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

            paddleLeft.Update(gameTime);
            paddleRight.Update(gameTime);
            pong.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));

            spriteBatch.Begin();
                spriteBatch.Draw(bgTexture, new Rectangle(0, 0, 640, 480), Color.White);
                foreach (Sprite s in spriteList)
                {
                    s.Draw(gameTime, spriteBatch);
                }
                pong.Draw(gameTime, spriteBatch);
                paddleLeft.Draw(gameTime, spriteBatch);
                paddleRight.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
