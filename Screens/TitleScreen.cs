using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Content;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace BaseGame
{
    class TitleScreen
    {
        Texture2D background, title, enter;

        Color enterColor;

        int framesCounter;

        //bool parpadeo;

        public TitleScreen(ContentManager Content) 
        {
            background = Content.Load<Texture2D>("background");
            title = Content.Load<Texture2D>("title");
            enter = Content.Load<Texture2D>("enter");

            framesCounter = 0;

            enterColor = new Color();
            enterColor = Color.White;
        }

        public void Update(KeyboardState keyState)
        {
            framesCounter++;

            /*if ((parpadeo == false) && (framesCounter >= 30))
            {
                framesCounter = 0;
                enterColor.A = 0;
                parpadeo = !parpadeo;
            }
            if ((parpadeo == true) && (framesCounter >= 30))
            {
                framesCounter = 0;
                enterColor.A = 255;
                parpadeo = !parpadeo;
            }*/
         
            if (keyState.IsKeyPressed(Keys.Enter)) BaseGame.gameScreen = GameScreen.GAMEPLAY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, Vector2.Zero, Color.YellowGreen);

            spriteBatch.Draw(title, new Vector2((BaseGame.screenWidth / 2) - (title.Width / 2), (BaseGame.screenHeight / 2) - (title.Height)), Color.White);
            if(((framesCounter / 30) %2) == 1)spriteBatch.Draw(enter, new Vector2((BaseGame.screenWidth / 2) - (enter.Width / 2), (BaseGame.screenHeight / 2) + (enter.Height)), enterColor);
        }
    }
}
