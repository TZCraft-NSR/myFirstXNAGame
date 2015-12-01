using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using VoidEngine.Effects;
using VoidEngine.Helpers;
using VoidEngine.VGUI;

namespace myFirstXNAGame
{
	/// <summary>
	/// This is the myFirstXNAGame class
	/// </summary>
	public class myFirstXNAGame : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        public KeyboardState keyboardState, pKeyboardState;
        
        // The left paddle
        PaddleLeft paddleLeft;
        // The right paddle
        PaddleRight paddleRight;
        // The right player paddle
        PaddleRight paddleRightPlayer;
        // The pong
        Pong pong;
        // The player object
        Player player;
        // The debug label
        Label debugLabel;
        // The score label
        Label score;
        // The game over label
        Label endText;
        // The reset button
        Button resetButton;
        // The pause/resume Button
        Button pauseButton;
        // The Main Menu button
        Button mainMenuButton;
        // The play button
        Button playButton;
        // The player vs player play button
        Button pvpPlayButton;
        // The exit button
        Button exitButton;
        // The options button
        Button optionsButton;
        // The cancel options button
        Button cancelButton;
        // The apply options button
        Button applyButton;
        // The background Label
        Label backgoundLabel;
        // The paddle left Label
        Label paddleLeftLabel;
        // The paddle right Label
        Label paddleRightLabel;
        // The pong label
        Label pongLabel;
        // The paused menu Label
        Label pausedLabel;
        // The Background Buttons
        Button bgColorRedUp;
        Button bgColorRedDown;
        Button bgColorGreenUp;
        Button bgColorGreenDown;
        Button bgColorBlueUp;
        Button bgColorBlueDown;
        // The paddle Left Buttons
        Button pLColorRedUp;
        Button pLColorRedDown;
        Button pLColorGreenUp;
        Button pLColorGreenDown;
        Button pLColorBlueUp;
        Button pLColorBlueDown;
        // The paddle Right Buttons
        Button pRColorRedUp;
        Button pRColorRedDown;
        Button pRColorGreenUp;
        Button pRColorGreenDown;
        Button pRColorBlueUp;
        Button pRColorBlueDown;
        // The pong Buttons
        Button pColorRedUp;
        Button pColorRedDown;
        Button pColorGreenUp;
        Button pColorGreenDown;
        Button pColorBlueUp;
        Button pColorBlueDown;
        // The random number generation
        public Random rng;
        // The bloom effect
        public BloomComponent bloom;
        // The main menu left paddle
        PaddleLeft dummyPaddleLeft;
        // The main menu right paddle
        PaddleRight dummyPaddleRight;
        // The main menu pong
        Pong dummyPong;
        // The start game instructions
        Label startGameInst;
        // The main menu button for reset
        Button mainMenuButton2;
		
        // The players texture
        Texture2D texturePlayer;
        // The pong texture
        Texture2D texturePong;
        // The background texture
        Texture2D bgTexture;
        // The button texture
        Texture2D buttonTexture;
        // The paddle wash texture
        Texture2D texturePaddleWash;
        // The bounding box texture
        Texture2D textureBox;
        // The Box Boxes box
        Texture2D textureColor;
        // The Arial sprite font
        SpriteFont arial;
        // The Arial bold sprite font
        SpriteFont arialBold;
        // The system sprite font
        SpriteFont system;

        // The scores
        public int scoreRight;
        public int scoreLeft;
		
        // If game is over
        public bool gameOver = false;
        // If player has won
        public bool playerWon = false;
        // If the game is able to start
        public bool ableStart = true;
        // The menu id.
        public int menu = 0;
        // The pong's position
        public Vector2 pongposition;
        // The main menu pong's position
        public Vector2 dummyPongposition;
        public Point windowSize;
        // The pong's default position
        public Vector2 pongDefaultposition;
        // If the game is paused
        public bool paused = false;
        // If the game is pvp
        public bool isPvp = false;
        // If the game has started
        public bool isStarted = false;
        // Elapsed Time
        public int elapsedTime;
        // Test button time
        public int testTime = 500;
        // Background Color
        public Color backgroundColor = new Color(0, 1f, 1f);
        public float bgColorR = 0;
        public float bgColorG = 1;
        public float bgColorB = 1;
        // Paddle Left Color
        public Color paddleLeftColor = new Color(0, 1f, 0);
        public float pLColorR = 0;
        public float pLColorG = 1;
        public float pLColorB = 0;
        // Paddle Right Color
        public Color paddleRightColor = new Color(1f, 0, 0);
        public float pRColorR = 1;
        public float pRColorG = 0;
        public float pRColorB = 0;
        // Pong Color
        public Color pongColor = new Color(1f, 0.80f, 0);
        public float pColorR = 1;
        public float pColorG = 0.80f;
        public float pColorB = 0;
		
        // The list of map segments
        public List<CollisionHelper.MapSegment> MapSegments;
		// The list of paddle segments
		public CollisionHelper.MapSegment[] PaddleSegments = new CollisionHelper.MapSegment[6];
		
        public myFirstXNAGame()
        {
            // Create the new graphics device manager
            graphics = new GraphicsDeviceManager(this);
            // Set the root directory of the content.
            Content.RootDirectory = "Content";
            // Set the screen size
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
            // Set the window size
            windowSize = new Point(640, 480);
            bloom = new BloomComponent(this);
            Components.Add(bloom);
        }
		
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Enables the mouse.
            IsMouseVisible = true;
			
            rng = new Random();
            MapSegments = new List<CollisionHelper.MapSegment>();

            MapSegments.Add(new CollisionHelper.MapSegment(new Point(1, 480), new Point(320, 479)));
            MapSegments.Add(new CollisionHelper.MapSegment(new Point(320, 479), new Point(640, 480)));
            MapSegments.Add(new CollisionHelper.MapSegment(new Point(640, 100), new Point(320, 101)));
            MapSegments.Add(new CollisionHelper.MapSegment(new Point(320, 101), new Point(1, 100)));
            MapSegments.Add(new CollisionHelper.MapSegment(new Point(1, 1), new Point(1, 480)));
            MapSegments.Add(new CollisionHelper.MapSegment(new Point(640, 480), new Point(640, 1)));

            // Creates the paddle segments.
            for (int i = 0; i < 6; i++)
            {
                PaddleSegments[i] = new CollisionHelper.MapSegment(new Point(0, 0), new Point(0, 1));
            }

            // Set the scores.
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
            texturePong = Content.Load<Texture2D>(@"Images\pong");
            bgTexture = Content.Load<Texture2D>(@"Images\background");
            buttonTexture = Content.Load<Texture2D>(@"Images\button");
            texturePaddleWash = Content.Load<Texture2D>(@"Images\paddleWash");
            textureBox = Content.Load<Texture2D>(@"Images\box");
            textureColor = Content.Load<Texture2D>(@"Images\boxGrey");
            arial = Content.Load<SpriteFont>(@"Fonts\Arial");
            arialBold = Content.Load<SpriteFont>(@"Fonts\Arial Bold");
            system = Content.Load<SpriteFont>(@"Fonts\System");
			
            // Creates the player character.
            player = new Player(new Vector2(100, graphics.PreferredBackBufferHeight - 50), 2, Keys.W, Keys.S, Keys.A, Keys.D, Keys.Q, Keys.E);
            // Creates the player animations
            player.addAnimations(texturePlayer);

            pongDefaultposition = new Vector2((windowSize.X - texturePong.Width) / 2, ((windowSize.Y - 100 - texturePong.Height) / 2) + 100);
			
            paddleLeft = new PaddleLeft(texturePaddleWash, new Vector2(25, (windowSize.Y - texturePaddleWash.Height) / 2), paddleLeftColor, this, Keys.W, Keys.S);

            paddleRight = new PaddleRight(texturePaddleWash, new Vector2((windowSize.X - 75), (windowSize.Y - texturePaddleWash.Height) / 2), paddleRightColor, this, Keys.None, Keys.None);

            paddleRightPlayer = new PaddleRight(texturePaddleWash, new Vector2((windowSize.X - 75), (windowSize.Y - texturePaddleWash.Height) / 2), paddleRightColor, this, Keys.Up, Keys.Down);
            paddleRightPlayer.ai = false;

            startGameInst = new Label(new Vector2(windowSize.X, 100), system, 0.5f, Color.White, "Press (Space) to start");

            pong = new Pong(texturePong, new Vector2((windowSize.X - texturePong.Width) / 2, ((windowSize.Y - 100 - texturePong.Height) / 2) + 100), pongColor, this);
            
            debugLabel = new Label(new Vector2(5, 5), arial, 0.5f, Color.White, "");

            // Creates the score text
            score = new Label(new Vector2(windowSize.X, 25), arial, 1.0f, Color.White, scoreLeft.ToString() + "     " + scoreRight.ToString());
			
            bgColorRedUp = new Button(buttonTexture, new Vector2(50, 100), arial, 1f, Color.Black, "Red +", Color.White);
            bgColorRedDown = new Button(buttonTexture, new Vector2(168, 100), arial, 1f, Color.Black, "Red -", Color.White);
            bgColorGreenUp = new Button(buttonTexture, new Vector2(50, 128), arial, 1f, Color.Black, "Green +", Color.White);
            bgColorGreenDown = new Button(buttonTexture, new Vector2(168, 128), arial, 1f, Color.Black, "Green -", Color.White);
            bgColorBlueUp = new Button(buttonTexture, new Vector2(50, 156), arial, 1f, Color.Black, "Blue +", Color.White);
            bgColorBlueDown = new Button(buttonTexture, new Vector2(168, 156), arial, 1f, Color.Black, "Blue -", Color.White);

            pLColorRedUp = new Button(buttonTexture, new Vector2(278, 100), arial, 1f, Color.Black, "Red +", Color.White);
            pLColorRedDown = new Button(buttonTexture, new Vector2(396, 100), arial, 1f, Color.Black, "Red -", Color.White);
            pLColorGreenUp = new Button(buttonTexture, new Vector2(278, 128), arial, 1f, Color.Black, "Green +", Color.White);
            pLColorGreenDown = new Button(buttonTexture, new Vector2(396, 128), arial, 1f, Color.Black, "Green -", Color.White);
            pLColorBlueUp = new Button(buttonTexture, new Vector2(278, 156), arial, 1f, Color.Black, "Blue +", Color.White);
            pLColorBlueDown = new Button(buttonTexture, new Vector2(396, 156), arial, 1f, Color.Black, "Blue -", Color.White);

            pRColorRedUp = new Button(buttonTexture, new Vector2(50, 264), arial, 1f, Color.Black, "Red +", Color.White);
            pRColorRedDown = new Button(buttonTexture, new Vector2(168, 264), arial, 1f, Color.Black, "Red -", Color.White);
            pRColorGreenUp = new Button(buttonTexture, new Vector2(50, 292), arial, 1f, Color.Black, "Green +", Color.White);
            pRColorGreenDown = new Button(buttonTexture, new Vector2(168, 292), arial, 1f, Color.Black, "Green -", Color.White);
            pRColorBlueUp = new Button(buttonTexture, new Vector2(50, 320), arial, 1f, Color.Black, "Blue +", Color.White);
            pRColorBlueDown = new Button(buttonTexture, new Vector2(168, 320), arial, 1f, Color.Black, "Blue -", Color.White);

            pColorRedUp = new Button(buttonTexture, new Vector2(278, 264), arial, 1f, Color.Black, "Red +", Color.White);
            pColorRedDown = new Button(buttonTexture, new Vector2(396, 264), arial, 1f, Color.Black, "Red -", Color.White);
            pColorGreenUp = new Button(buttonTexture, new Vector2(278, 292), arial, 1f, Color.Black, "Green +", Color.White);
            pColorGreenDown = new Button(buttonTexture, new Vector2(396, 292), arial, 1f, Color.Black, "Green -", Color.White);
            pColorBlueUp = new Button(buttonTexture, new Vector2(278, 320), arial, 1f, Color.Black, "Blue +", Color.White);
            pColorBlueDown = new Button(buttonTexture, new Vector2(396, 320), arial, 1f, Color.Black, "Blue -", Color.White);

            applyButton = new Button(buttonTexture, new Vector2(((windowSize.X - buttonTexture.Width) / 2) + 15, windowSize.Y - 28), arial, 1.0f, Color.Black, "Apply", Color.White);
            cancelButton = new Button(buttonTexture, new Vector2(((windowSize.X - buttonTexture.Width) / 2) + ((buttonTexture.Width / 2) + 15), windowSize.Y - 28), arial, 1.0f, Color.Black, "Cancel", Color.White);

            backgoundLabel = new Label(new Vector2(80, 75), arial, 1.0f, Color.White, "Background");
            paddleLeftLabel = new Label(new Vector2(600, 75), arial, 1.0f, Color.White, "Paddle Left");
            paddleRightLabel = new Label(new Vector2(80, 239), arial, 1.0f, Color.White, "Paddle Right");
            pongLabel = new Label(new Vector2(650, 239), arial, 1.0f, Color.White, "Pong");
			
            endText = new Label(new Vector2(windowSize.X, ((windowSize.Y - 23) / 2) - 28), arial, 1.0f, Color.White, "");

            resetButton = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, (windowSize.Y - 23) / 2), arial, 1.0f, Color.Black, "Reset", Color.White);

            mainMenuButton2 = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, ((windowSize.Y - 23) / 2) + 28), arial, 1f, Color.Black, "Menu", Color.White);
			
            pauseButton = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, ((windowSize.Y - 23) / 2)), arial, 1.0f, Color.Black, "Resume", Color.White);
            mainMenuButton = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, ((windowSize.Y - 23) / 2) + 28), arial, 1.0f, Color.Black, "Menu", Color.White);
            pausedLabel = new Label(new Vector2(windowSize.X, ((windowSize.Y - 20) / 2) - 48), arial, 1f, Color.White, "Paused");
			
            playButton = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, 150), arial, 1.0f, Color.Black, "Play", Color.White);

            pvpPlayButton = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, 178), arial, 1.0f, Color.Black, "Play PvP", Color.White);

            optionsButton = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, 206), arial, 1.0f, Color.Black, "Options", Color.White);

            exitButton = new Button(buttonTexture, new Vector2((windowSize.X - 85) / 2, 243), arial, 1.0f, Color.Black, "Exit", Color.White);


            dummyPaddleLeft = new PaddleLeft(texturePaddleWash, new Vector2(25, (windowSize.Y - texturePaddleWash.Height) / 2), new Color(0, 1f, 0), this, Keys.None, Keys.None);
            dummyPaddleLeft.dummy = true;
            dummyPaddleRight = new PaddleRight(texturePaddleWash, new Vector2((windowSize.X - 75), (windowSize.Y - texturePaddleWash.Height) / 2), Color.Red, this, Keys.None, Keys.None);
            dummyPaddleRight.dummy = true;
            dummyPong = new Pong(texturePong, new Vector2((windowSize.X - texturePong.Width) / 2, ((windowSize.Y - 100 - texturePong.Height) / 2) + 100), new Color(1, 0.80f, 0), this);
            dummyPong.move = true;
            dummyPong.dummy = true;
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
			
            // Updates the sprites in the sprite list
            paddleLeft.Update(gameTime);
            if (isPvp)
            {
                paddleRightPlayer.Update(gameTime);
            }
            else
            {
                paddleRight.Update(gameTime);
            }
            pong.Update(gameTime);
             
            pKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
			
            if (menu == 1)
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    if (paused && elapsedTime >= testTime)
                    {
                        paused = false;
                        elapsedTime = 0;
                    }
                    else if (!paused && elapsedTime >= testTime)
                    {
                        paused = true;
                        elapsedTime = 0;
                    }
                }
            }

			// Updates the score
			score.Text = scoreLeft.ToString() + "     " + scoreRight.ToString();
			score.Update(gameTime);

			bloom.Update(gameTime);
			
            dummyPaddleLeft.Update(gameTime);
            dummyPaddleRight.Update(gameTime);
            dummyPong.Update(gameTime);

            dummyPongposition = dummyPong.position;
			
            debugLabel.Update(gameTime, paddleRightPlayer.ai.ToString() + " " + paddleRightPlayer.dummy.ToString() + " " + paddleRightPlayer.move.ToString() + " " + isPvp.ToString());
			
            // Sees if the game is ended
            if (gameOver == true)
            {
                ableStart = false;

                if (playerWon == true)
                {
                    endText.Text = "You Won!!!";
					endText.Update(gameTime);
                }
                else
                {
                    if (isPvp)
                    {
						endText.Text = "Player 2 Won!!!";
						endText.Update(gameTime);
					}
                    else
                    {
						endText.Text = "CPU Won!!!";
						endText.Update(gameTime);
					}
                }
            }
			
            resetButton.Update(gameTime);
            playButton.Update(gameTime);
            optionsButton.Update(gameTime);
            exitButton.Update(gameTime);
            pauseButton.Update(gameTime);
            mainMenuButton.Update(gameTime);
            pvpPlayButton.Update(gameTime);
            mainMenuButton2.Update(gameTime);
            bgColorRedUp.Update(gameTime);
            bgColorRedDown.Update(gameTime);
            bgColorGreenUp.Update(gameTime);
            bgColorGreenDown.Update(gameTime);
            bgColorBlueUp.Update(gameTime);
            bgColorBlueDown.Update(gameTime);
            pLColorRedUp.Update(gameTime);
            pLColorRedDown.Update(gameTime);
            pLColorGreenUp.Update(gameTime);
            pLColorGreenDown.Update(gameTime);
            pLColorBlueUp.Update(gameTime);
            pLColorBlueDown.Update(gameTime);
            pRColorRedUp.Update(gameTime);
            pRColorRedDown.Update(gameTime);
            pRColorGreenUp.Update(gameTime);
            pRColorGreenDown.Update(gameTime);
            pRColorBlueUp.Update(gameTime);
            pRColorBlueDown.Update(gameTime);
            pColorRedUp.Update(gameTime);
            pColorRedDown.Update(gameTime);
            pColorGreenUp.Update(gameTime);
            pColorGreenDown.Update(gameTime);
            pColorBlueUp.Update(gameTime);
            pColorBlueDown.Update(gameTime);
            applyButton.Update(gameTime);
            cancelButton.Update(gameTime);
			
            if (resetButton.Clicked() && menu == 1 && !paused && gameOver)
            {
                Reset();
            }
            else if (mainMenuButton2.Clicked() && menu == 1 && !paused && gameOver)
            {
                menu = 0;
            }
			
            if (pvpPlayButton.Clicked() && menu == 0)
            {
                menu = 1;
                isPvp = true;
                Reset();
            }
            else if (playButton.Clicked() && menu == 0)
            {
                menu = 1;
                isPvp = false;
                Reset();
            }
            else if (optionsButton.Clicked() && menu == 0)
            {
                menu = 2;
            }
            else if (exitButton.Clicked() && menu == 0)
            {
                this.Exit();
            }

            if (pauseButton.Clicked() && menu == 1 && paused)
            {
                paused = false;
            }
            else if (mainMenuButton.Clicked() && menu == 1 && paused)
            {
                menu = 0;
            }

            if (bgColorRedUp.Clicked() && menu == 2)
            {
                bgColorR += 0.1f;
            }
            else if (bgColorRedDown.Clicked() && menu == 2)
            {
                bgColorR -= 0.1f;
            }
            else if (bgColorGreenUp.Clicked() && menu == 2)
            {
                bgColorG += 0.1f;
            }
            else if (bgColorGreenDown.Clicked() && menu == 2)
            {
                bgColorG -= 0.1f;
            }
            else if (bgColorBlueUp.Clicked() && menu == 2)
            {
                bgColorB += 0.1f;
            }
            else if (bgColorBlueDown.Clicked() && menu == 2)
            {
                bgColorB -= 0.1f;
            }

            if (pLColorRedUp.Clicked() && menu == 2)
            {
                pLColorR += 0.1f;
            }
            else if (pLColorRedDown.Clicked() && menu == 2)
            {
                pLColorR -= 0.1f;
            }
            else if (pLColorGreenUp.Clicked() && menu == 2)
            {
                pLColorG += 0.1f;
            }
            else if (pLColorGreenDown.Clicked() && menu == 2)
            {
                pLColorG -= 0.1f;
            }
            else if (pLColorBlueUp.Clicked() && menu == 2)
            {
                pLColorB += 0.1f;
            }
            else if (pLColorBlueDown.Clicked() && menu == 2)
            {
                pLColorB -= 0.1f;
            }

            if (pRColorRedUp.Clicked() && menu == 2)
            {
                pRColorR += 0.1f;
            }
            else if (pRColorRedDown.Clicked() && menu == 2)
            {
                pRColorR -= 0.1f;
            }
            else if (pRColorGreenUp.Clicked() && menu == 2)
            {
                pRColorG += 0.1f;
            }
            else if (pRColorGreenDown.Clicked() && menu == 2)
            {
                pRColorG -= 0.1f;
            }
            else if (pRColorBlueUp.Clicked() && menu == 2)
            {
                pRColorB += 0.1f;
            }
            else if (pRColorBlueDown.Clicked() && menu == 2)
            {
                pRColorB -= 0.1f;
            }

            if (pColorRedUp.Clicked() && menu == 2)
            {
                pColorR += 0.1f;
            }
            else if (pColorRedDown.Clicked() && menu == 2)
            {
                pColorR -= 0.1f;
            }
            else if (pColorGreenUp.Clicked() && menu == 2)
            {
                pColorG += 0.1f;
            }
            else if (pColorGreenDown.Clicked() && menu == 2)
            {
                pColorG -= 0.1f;
            }
            else if (pColorBlueUp.Clicked() && menu == 2)
            {
                pColorB += 0.1f;
            }
            else if (pColorBlueDown.Clicked() && menu == 2)
            {
                pColorB -= 0.1f;
            }

            if (applyButton.Clicked() && menu == 2)
            {
                backgroundColor = new Color(bgColorR, bgColorG, bgColorB);
                paddleLeftColor = new Color(pLColorR, pLColorG, pLColorB);
                paddleRightColor = new Color(pRColorR, pRColorG, pRColorB);
                pongColor = new Color(pColorR, pColorG, pColorB);
                pong.color = pongColor;
                paddleLeft.color = paddleLeftColor;
                paddleRight.color = paddleRightColor;
                menu = 0;
            }
            else if (cancelButton.Clicked() && menu == 2)
            {
                backgroundColor = new Color(0, 1f, 1f);
                paddleLeftColor = new Color(0, 1f, 0);
                paddleRightColor = new Color(1f, 0, 0);
                pongColor = new Color(1f, 0.80f, 0);
                pong.color = pongColor;
                paddleLeft.color = paddleLeftColor;
                paddleRight.color = paddleRightColor;
                menu = 0;
            }

            base.Update(gameTime);
            // TODO: Add your update logic here
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            GraphicsDevice.Clear(new Color(50, 50, 50));
			
            spriteBatch.Begin(0, BlendState.Opaque);
                bloom.Enabled = true;
                bloom.BeginDraw();
                if (menu == 0)
                {
                    spriteBatch.Draw(bgTexture, Vector2.Zero, Color.Red);
                }
                else if (menu == 1)
                {

                    spriteBatch.Draw(bgTexture, Vector2.Zero, backgroundColor);
                }
                else if (menu == 2)
                {
                    spriteBatch.Draw(bgTexture, Vector2.Zero, new Color(0, 1f, 0));
                }
            spriteBatch.End();

            spriteBatch.Begin();
                bloom.BeginDraw();
                if (menu == 0)
                {
                    dummyPaddleLeft.Draw(gameTime, spriteBatch);
                    dummyPaddleRight.Draw(gameTime, spriteBatch);
                    dummyPong.Draw(gameTime, spriteBatch);
                    spriteBatch.DrawString(system, "Neon Pong", new Vector2((windowSize.X) / 2, 25), Color.White, 0, new Vector2(250, 0), 1.0f, SpriteEffects.None, 0f);
                }
                else if (menu == 1)
                {
                    paddleLeft.Draw(gameTime, spriteBatch);
                    if (isPvp)
                    {
                        paddleRightPlayer.Draw(gameTime, spriteBatch);
                    }
                    else
                    {
                        paddleRight.Draw(gameTime, spriteBatch);
                    }
                    pong.Draw(gameTime, spriteBatch);
                }
                else if (menu == 2)
                {

                }
            spriteBatch.End();

            device.DepthStencilState = DepthStencilState.Default;

            base.Draw(gameTime);
			
            spriteBatch.Begin();
                /*
                spriteBatch.Draw(textureBox, new Rectangle(MapSegments[0].point1.X, MapSegments[0].point1.Y, MapSegments[0].point2.X, MapSegments[0].point2.Y), Color.White);
                spriteBatch.Draw(textureBox, new Rectangle(MapSegments[1].point1.X, MapSegments[1].point1.Y, MapSegments[1].point2.X, MapSegments[1].point2.Y), Color.White);
                spriteBatch.Draw(textureBox, new Rectangle(MapSegments[2].point1.X, MapSegments[2].point1.Y, MapSegments[2].point2.X, MapSegments[2].point2.Y), Color.White);
                spriteBatch.Draw(textureBox, new Rectangle(MapSegments[3].point1.X, MapSegments[3].point1.Y, MapSegments[3].point2.X, MapSegments[3].point2.Y), Color.White);
                spriteBatch.Draw(textureBox, new Rectangle(MapSegments[4].point1.X, MapSegments[4].point1.Y, MapSegments[4].point2.X, MapSegments[4].point2.Y), Color.White);
                spriteBatch.Draw(textureBox, new Rectangle(MapSegments[5].point1.X, MapSegments[5].point1.Y, MapSegments[5].point2.X, MapSegments[5].point2.Y), Color.White);
                for (int i = 0; i > 6; i++)
                {
                    spriteBatch.Draw(textureBox, new Rectangle(PaddleSegments[i].point1.X, PaddleSegments[i].point1.Y, PaddleSegments[i].point2.X, PaddleSegments[i].point2.Y), Color.White);
                }
                 */
                //debugLabel.Draw(gameTime, spriteBatch);
                if (menu == 0)
                {
                    playButton.Draw(gameTime, spriteBatch);
                    optionsButton.Draw(gameTime, spriteBatch);
                    exitButton.Draw(gameTime, spriteBatch);
                    pvpPlayButton.Draw(gameTime, spriteBatch);
                }
                else if (menu == 1)
                {
                    score.Draw(gameTime, spriteBatch);

                    if (gameOver && !paused)
                    {
                        endText.Draw(gameTime, spriteBatch);
                        resetButton.Draw(gameTime, spriteBatch);
                        mainMenuButton2.Draw(gameTime, spriteBatch);
                    }
                    if (paused)
                    {
                        pauseButton.Draw(gameTime, spriteBatch);
                        paddleLeft.move = false;
                        paddleRight.move = false;
                        pong.move = false;
                        paused = true;
                        mainMenuButton.Draw(gameTime, spriteBatch);
                    }
                    if (!paused && isStarted && !gameOver)
                    {
                        paddleLeft.move = true;
                        paddleRight.move = true;
                        pong.move = true;
                        paused = false;
                    }

                    if (ableStart && !gameOver && !isStarted)
                    {
                        startGameInst.Draw(gameTime, spriteBatch);
                    }
                }
                else if (menu == 2)
                {
                    bgColorRedUp.Draw(gameTime, spriteBatch);
                    bgColorRedDown.Draw(gameTime, spriteBatch);
                    bgColorGreenUp.Draw(gameTime, spriteBatch);
                    spriteBatch.Draw(textureColor, new Rectangle(140, 128, 23, 23), new Color(bgColorR, bgColorG, bgColorB));
                    bgColorGreenDown.Draw(gameTime, spriteBatch);
                    bgColorBlueUp.Draw(gameTime, spriteBatch);
                    bgColorBlueDown.Draw(gameTime, spriteBatch);
                    pLColorRedUp.Draw(gameTime, spriteBatch);
                    pLColorRedDown.Draw(gameTime, spriteBatch);
                    pLColorGreenUp.Draw(gameTime, spriteBatch);
                    spriteBatch.Draw(textureColor, new Rectangle(368, 128, 23, 23), new Color(pLColorR, pLColorG, pLColorB));
                    pLColorGreenDown.Draw(gameTime, spriteBatch);
                    pLColorBlueUp.Draw(gameTime, spriteBatch);
                    pLColorBlueDown.Draw(gameTime, spriteBatch);
                    pRColorRedUp.Draw(gameTime, spriteBatch);
                    pRColorRedDown.Draw(gameTime, spriteBatch);
                    pRColorGreenUp.Draw(gameTime, spriteBatch);
                    spriteBatch.Draw(textureColor, new Rectangle(140, 292, 23, 23), new Color(pRColorR, pRColorG, pRColorB));
                    pRColorGreenDown.Draw(gameTime, spriteBatch);
                    pRColorBlueUp.Draw(gameTime, spriteBatch);
                    pRColorBlueDown.Draw(gameTime, spriteBatch);
                    pColorRedUp.Draw(gameTime, spriteBatch);
                    pColorRedDown.Draw(gameTime, spriteBatch);
                    pColorGreenUp.Draw(gameTime, spriteBatch);
                    spriteBatch.Draw(textureColor, new Rectangle(368, 292, 23, 23), new Color(pColorR, pColorG, pColorB));
                    pColorGreenDown.Draw(gameTime, spriteBatch);
                    pColorBlueUp.Draw(gameTime, spriteBatch);
                    pColorBlueDown.Draw(gameTime, spriteBatch);
                    applyButton.Draw(gameTime, spriteBatch);
                    cancelButton.Draw(gameTime, spriteBatch);
                    backgoundLabel.Draw(gameTime, spriteBatch);
                    paddleLeftLabel.Draw(gameTime, spriteBatch);
                    paddleRightLabel.Draw(gameTime, spriteBatch);
                    pongLabel.Draw(gameTime, spriteBatch);
                }
            spriteBatch.End();

            // TODO: Add your drawing code here
        }
		
        public void Reset()
        {
            gameOver = false;
            paused = false;
            playerWon = false;
            scoreLeft = 0;
            scoreRight = 0;
            paddleLeft.move = false;
            paddleRight.move = false;
            pong.move = false;
            pong.speed = 3;
            ableStart = true;
            paddleLeft.position = new Vector2(25, (windowSize.Y - texturePaddleWash.Height) / 2);
            paddleRight.position = new Vector2(windowSize.X - 75, (windowSize.Y - texturePaddleWash.Height) / 2);
            pong.position = new Vector2((windowSize.X - texturePong.Width) / 2, ((windowSize.Y - 100 - texturePong.Height) / 2) + 100);
        }
    }
}