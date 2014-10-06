using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace BaseGame
{
    public enum GameScreen
    {
        LOGO, TITLE, GAMEPLAY, ENDING
    }
    
    public class BaseGame : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        private KeyboardManager keyboard;
        private KeyboardState keyboardState;

        private MouseManager mouse;
        private MouseState mouseState;

        public const int screenWidth = 1280;
        public const int screenHeight = 720;
        
        public static GameScreen gameScreen;
        
        // Screens Objects
        LogoScreen logoScreen;
        TitleScreen titleScreen;
        GameplayScreen gameplayScreen;
        EndingScreen endingScreen;

        Color raywhite = new Color(245, 245, 245, 255);

        float time;

        public BaseGame()
        {
            // Create the graphics device manager
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            // Setup the relative directory to the executable directory for content loading with ContentManager
            Content.RootDirectory = "Content";

            // Initialize input keyboard system
            keyboard = new KeyboardManager(this);

            // Initialize input mouse system
            mouse = new MouseManager(this);
        }

        protected override void Initialize()
        {
            // Setup window size and apply changes
            graphicsDeviceManager.PreferredBackBufferWidth = screenWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = screenHeight;
            graphicsDeviceManager.ApplyChanges();

            Window.Title = "SharpDX Base Game Screens";
            
            // Instantiate a SpriteBatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameScreen = GameScreen.LOGO;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load a system font

            
            // Create Screens Objects
            logoScreen = new LogoScreen(Content);
            titleScreen = new TitleScreen(Content);
            gameplayScreen = new GameplayScreen(Content);
            endingScreen = new EndingScreen(Content);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // Unload spriteBatch object
            spriteBatch.Dispose();

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Get the current state of the keyboard
            keyboardState = keyboard.GetState();

            // Get the current state of the mouse
            mouseState = mouse.GetState();

            IsMouseVisible = true;

            // Check if Escape key has been pressed to exit
            if (keyboardState.IsKeyPressed(Keys.Escape)) Exit();

            // Count total execution time in seconds
            time = (float)gameTime.TotalGameTime.TotalSeconds;
            
            // Game screens update
            switch (gameScreen)
            {
                case GameScreen.LOGO: logoScreen.Update(keyboardState); break;
                case GameScreen.TITLE: titleScreen.Update(keyboardState); break;
                case GameScreen.GAMEPLAY: gameplayScreen.Update(keyboardState); break;
                case GameScreen.ENDING: endingScreen.Update(keyboardState); break;
                default: break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(raywhite);

            spriteBatch.Begin(SpriteSortMode.Deferred, GraphicsDevice.BlendStates.NonPremultiplied);

            // Game screens draw
            switch (gameScreen)
            {
                case GameScreen.LOGO: logoScreen.Draw(spriteBatch); break;
                case GameScreen.TITLE: titleScreen.Draw(spriteBatch); break;
                case GameScreen.GAMEPLAY: gameplayScreen.Draw(spriteBatch); break;
                case GameScreen.ENDING: endingScreen.Draw(spriteBatch); break;
                default: break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
