using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace myFirstXNAGame
{
	public  class Sprite
    {
        Point frameSize;
        Point sheetSize;
        int fTime;
        Point currentFrame;
        int lFTime;
        Texture2D texture;
        Vector2 position;

        public Sprite(Texture2D tex, Vector2 pos, int frameWidth, int frameHeight, int sheetWidth, int sheetHeight, int fps)
        {
            frameSize = new Point(frameWidth, frameHeight);
            sheetSize = new Point(sheetWidth, sheetHeight);
            fTime = fps;

            texture = tex;
            position = pos;

            currentFrame = Point.Zero;
            lFTime = 0;
        }

        public Sprite(Texture2D tex, Vector2 pos)
        {
            frameSize = new Point(60, 50);
            sheetSize = new Point(5, 6);
            fTime = 16;

            texture = tex;
            position = pos;

            currentFrame = Point.Zero;
            lFTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            lFTime += gameTime.ElapsedGameTime.Milliseconds;

            if (lFTime >= fTime)
            {
                currentFrame.X++;

                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.Y++;
                    currentFrame.X = 0;

                    if (currentFrame.Y >= sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }

                lFTime = 0;
            }
        }

        /// <summary>
        /// Put inbetween the spriteBatch.Begin and spriteBatch.End
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
        }
    }
}
