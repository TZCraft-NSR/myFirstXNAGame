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
        public GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        public List<Sprite> spriteList;
        Player player;
        Label debugLabel;
        Label score;
        Label endText;
        Button resetButton;
        Button playButton;
        Button exitButton;
        Button optionsButton;

        Texture2D texturePlayer;
        Texture2D texturePaddle;
        Texture2D texturePong;
        Texture2D bgTexture;
        Texture2D buttonTexture;
        SpriteFont arial;
        SpriteFont arialBold;

        public int scoreRight;
        public int scoreLeft;

        public bool gameOver = false;
        public bool playerWon = false;
        public bool ableStart = true;

        public int menu = 0;

        public Random rng;

        public List<Collision.MapSegment> MapSegments;
        public Collision.MapSegment[] PaddleSegments = new Collision.MapSegment[6];

        public Vector2 pongPosition;

        public Point windowSize;

        MouseState current_mouse = Mouse.GetState();

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

            MapSegments.Add(new Collision.MapSegment(new Point(1, 480), new Point(320, 479)));
            MapSegments.Add(new Collision.MapSegment(new Point(320, 479), new Point(640, 480)));
            MapSegments.Add(new Collision.MapSegment(new Point(640, 100), new Point(320, 101)));
            MapSegments.Add(new Collision.MapSegment(new Point(320, 101), new Point(1, 100)));
            MapSegments.Add(new Collision.MapSegment(new Point(1, 1), new Point(1, 480)));
            MapSegments.Add(new Collision.MapSegment(new Point(640, 480), new Point(640, 1)));

            for (int i = 0; i < 6; i++)
            {
                PaddleSegments[i] = new Collision.MapSegment(new Point(0, 0), new Point(0, 1));
            }

            scoreLeft = 0;
            scoreRight = 0;

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

            texturePlayer = Content.Load<Texture2D>(@"Images\john");
            texturePaddle = Content.Load<Texture2D>(@"Images\paddle");
            texturePong = Content.Load<Texture2D>(@"Images\pong");
            bgTexture = Content.Load<Texture2D>(@"Images\background");
            buttonTexture = Content.Load<Texture2D>(@"Images\button");

            arial = Content.Load<SpriteFont>(@"Fonts\Arial");
            arialBold = Content.Load<SpriteFont>(@"Fonts\Arial Bold");

            player = new Player(new Vector2(100, graphics.PreferredBackBufferHeight - 50), 2, Keys.W, Keys.S, Keys.A, Keys.D, Keys.Q, Keys.E);
            player.AddAnimations(texturePlayer);

            spriteList.Add(new PaddleLeft(texturePaddle, new Vector2(25, (windowSize.Y - texturePaddle.Height) / 2), this, Keys.W, Keys.S));

            spriteList.Add(new PaddleRight(texturePaddle, new Vector2((windowSize.X - 75), (windowSize.Y - texturePaddle.Height) / 2), this, Keys.None, Keys.None));

            spriteList.Add(new Pong(texturePong, new Vector2((windowSize.X - texturePong.Width) / 2, ((windowSize.Y - 100 - texturePong.Height) / 2) + 100), this));

            debugLabel = new Label(new Vector2(5, 5), arial, 0.5f, spriteList[2].direction.X.ToString() + " " + spriteList[2].direction.Y.ToString() + " " + spriteList[2].speed.ToString() + " " + spriteList[2].position.X.ToString() + " " + spriteList[2].position.Y.ToString() + " " + (spriteList[1].position.X - spriteList[1].currentAnimation.frameSize.X) + " " + spriteList[1].position.Y + spriteList[1].currentAnimation.frameSize.Y);

            score = new Label(new Vector2(((windowSize.X - 50) / 2), 25), arial, 1.0f, scoreLeft.ToString() + "     " + scoreRight.ToString());

            endText = new Label(new Vector2((windowSize.X - 160) / 2, (windowSize.Y - 50) / 2), arial, 1.0f, "");

            resetButton = new Button(new Vector2((windowSize.X - 85) / 2, (windowSize.Y - 23) / 2), arial, 1.0f, "Reset");
            resetButton.AddAnimations(buttonTexture);

            playButton = new Button(new Vector2((windowSize.X - 85) / 2, 150), arial, 1.0f, "Play");
            playButton.AddAnimations(buttonTexture);

            optionsButton = new Button(new Vector2((windowSize.X - 85) / 2, 178), arial, 1.0f, "Options");
            optionsButton.AddAnimations(buttonTexture);

            exitButton = new Button(new Vector2((windowSize.X - 85) / 2, 178 + 23 + 5), arial, 1.0f, "Exit");
            exitButton.AddAnimations(buttonTexture);
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

            score.Update(gameTime, scoreLeft.ToString() + "     " + scoreRight.ToString());

            debugLabel.Update(gameTime, spriteList[2].direction.X.ToString() + " " + spriteList[2].direction.Y.ToString() + " " + spriteList[2].speed.ToString() + " " + spriteList[2].position.X.ToString() + " " + spriteList[2].position.Y.ToString() + " " + resetButton.position.X.ToString() + " " + resetButton.position.Y.ToString() + " " + resetButton.mX.ToString() + " " + resetButton.mY.ToString());
            // TODO: Add your update logic here

            base.Update(gameTime);

            if (gameOver == true)
            {
                ableStart = false;

                if (playerWon == true)
                {
                    endText.Update(gameTime, "You won!!!");
                }
                else
                {
                    endText.Update(gameTime, "Player2/CPU Won!!!");
                }
            }

            resetButton.Update(gameTime);
            playButton.Update(gameTime);
            optionsButton.Update(gameTime);
            exitButton.Update(gameTime);

            if (resetButton.Clicked())
            {
                Reset();
            }

            if (playButton.Clicked())
            {
                menu = 1;
            }
            else if (optionsButton.Clicked())
            {
                menu = 0;
            }
            if (exitButton.Clicked())
            {
                this.Exit();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));

            spriteBatch.Begin();
                if (menu == 0)
                {
                    spriteBatch.Draw(bgTexture, Vector2.Zero, Color.Red);

                    playButton.Draw(gameTime, spriteBatch);
                    optionsButton.Draw(gameTime, spriteBatch);
                    exitButton.Draw(gameTime, spriteBatch);
                }
                else if (menu == 1)
                {
                    spriteBatch.Draw(bgTexture, Vector2.Zero, Color.White);

                    foreach (Sprite s in spriteList)
                    {
                        s.Draw(gameTime, spriteBatch);
                    }
                    //label1.Draw(gameTime, spriteBatch);
                    score.Draw(gameTime, spriteBatch);
                    if (gameOver == true)
                    {
                        endText.Draw(gameTime, spriteBatch);
                        resetButton.Draw(gameTime, spriteBatch);
                    }
                }
                else if (menu == 2)
                {

                }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void Reset()
        {
            gameOver = false;
            playerWon = false;
            scoreLeft = 0;
            scoreRight = 0;
            spriteList[0].move = false;
            spriteList[1].move = false;
            spriteList[1].moveAi = false;
            spriteList[2].move = false;
            ableStart = true;
            spriteList[0].position = new Vector2(25, (windowSize.Y - texturePaddle.Height) / 2);
            spriteList[1].position = new Vector2((windowSize.X - 75), (windowSize.Y - texturePaddle.Height) / 2);
            spriteList[2].position = new Vector2((windowSize.X - texturePong.Width) / 2, ((windowSize.Y - 100 - texturePong.Height) / 2) + 100);
        }
    }
}
