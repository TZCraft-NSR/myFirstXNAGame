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

        SpriteFont font1;

        //Texture2D texturePlayer;
        Texture2D texturePaddle;
        Texture2D texturePong;
        Texture2D bgTexture;

        public Random rng;

        public List<Collision.MapSegment> MapSegments;

        public Point windowSize;

        public myFirstXNAGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;

            windowSize = new Point(640, 480);
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

            spriteList = new List<Sprite>();
            rng = new Random();
            MapSegments = new List<Collision.MapSegment>();

            MapSegments.Add(new Collision.MapSegment(new Point(0, 480), new Point(320, 479)));
            MapSegments.Add(new Collision.MapSegment(new Point(320, 479), new Point(640, 480)));
            MapSegments.Add(new Collision.MapSegment(new Point(640, 100), new Point(320, 101)));
            MapSegments.Add(new Collision.MapSegment(new Point(320, 101), new Point(0, 100)));
            MapSegments.Add(new Collision.MapSegment(new Point(0, 0), new Point(0, 480)));
            MapSegments.Add(new Collision.MapSegment(new Point(640, 480), new Point(640, 0)));

            // TODO: Add your initialization logic here

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

            //texturePlayer = Content.Load<Texture2D>(@"Images\john");
            texturePaddle = Content.Load<Texture2D>(@"Images\paddle");
            texturePong = Content.Load<Texture2D>(@"Images\pong");
            bgTexture = Content.Load<Texture2D>(@"Images\background");
            font1 = Content.Load<SpriteFont>(@"Fonts\Courier New");

            //player = new Player(new Vector2(100, graphics.PreferredBackBufferHeight - 50), 2, Keys.W, Keys.S, Keys.A, Keys.D, Keys.Q, Keys.E);
            //player.drawDebug(font1);
            //player.AddAnimations(texturePlayer);
            spriteList.Add(new PaddleLeft(texturePaddle, new Vector2(25, (windowSize.Y - texturePaddle.Height) / 2), this, Keys.W, Keys.S));
            spriteList.Add(new PaddleRight(texturePaddle, new Vector2((windowSize.X - 75), (windowSize.Y - texturePaddle.Height) / 2), this, Keys.None, Keys.None));
            spriteList.Add(new Pong(texturePong, new Vector2((windowSize.X - 40) / 2, ((windowSize.Y - 100 - texturePong.Height) / 2) + 100), this));
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
            GraphicsDevice.Clear(new Color(50, 50, 50));

            spriteBatch.Begin();
                spriteBatch.Draw(bgTexture, Vector2.Zero, Color.White);

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
