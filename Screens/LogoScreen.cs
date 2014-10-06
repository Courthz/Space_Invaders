using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Content;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace BaseGame
{
    class LogoScreen
    {
        Texture2D background, logo;

        int framesCounter;

        Color logoColor;

        public LogoScreen(ContentManager Content) 
        {
            background = Content.Load<Texture2D>("background");
            logo = Content.Load<Texture2D>("logo");

            framesCounter = 0;

            logoColor = new Color();
            logoColor = Color.White;
            logoColor.A = 0;
        }

        public void Update(KeyboardState keyboardState)
        {
            framesCounter++;

            if (keyboardState.IsKeyPressed(Keys.Enter)) BaseGame.gameScreen = GameScreen.TITLE;

            if (framesCounter <= 120) logoColor.A++;
            if ((framesCounter > 120) && (framesCounter <= 240)) logoColor.A = logoColor.A;
            if ((framesCounter > 240) && (framesCounter <= 360)) logoColor.A--;
            if (framesCounter > 360) BaseGame.gameScreen = GameScreen.TITLE;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.Wheat);

            spriteBatch.Draw(logo, new Vector2((BaseGame.screenWidth / 2) - (logo.Width / 2), (BaseGame.screenHeight / 2) - (logo.Height / 2)), logoColor);
        }
    }
}
