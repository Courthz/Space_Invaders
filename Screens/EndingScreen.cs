using SharpDX;
using SharpDX.Toolkit.Content;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace BaseGame
{
    class EndingScreen
    {
        Texture2D background, gameover;

        int framesCounter;

        Color endingColor;

        public EndingScreen(ContentManager Content)
        {
            background = Content.Load<Texture2D>("background");
            gameover = Content.Load<Texture2D>("gameover");

            framesCounter = 0;

            endingColor = new Color();
            endingColor = Color.Red;
            endingColor.A = 0;
        }

        public void Update(KeyboardState keyState)
        {
            framesCounter++;

            if (framesCounter <= 120) endingColor.A++;
            if ((framesCounter > 120) && (framesCounter <= 240)) endingColor.A = endingColor.A;
            if ((framesCounter > 240) && (framesCounter <= 360)) endingColor.A--;
            if (keyState.IsKeyPressed(Keys.Enter))BaseGame.gameScreen = GameScreen.TITLE;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.Black);

            spriteBatch.Draw(gameover, new Vector2((BaseGame.screenWidth / 2) - (gameover.Width / 2), (BaseGame.screenHeight / 2) - (gameover.Height / 2)), endingColor);
        }
    }
}
