using SharpDX;
using SharpDX.Toolkit.Content;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using System;
using System.IO;

namespace BaseGame
{
    class GameplayScreen
    {
        Player player;
        Enemy[] enemies;
        SinEnemy[] enemiesSin;

        Texture2D playertex, enemytex, espacio, playerlife, shottex, explosion;

        Random random;

        SpriteFont font;

        int playerDeadCounter = 0;
        int framesCounter = 0;
        int secondsCounter = 99;
        int score = 0;
        int deadEnemiesCounter = 0;
        int hiScore;
        const int maxEnemies = 20;
        const int maxEnemiesSin = 5;


        public GameplayScreen(ContentManager Content) 
        {
            random = new Random();
            espacio = Content.Load<Texture2D>("espacio");
            playertex = Content.Load<Texture2D>("player");
            enemytex = Content.Load<Texture2D>("enemy");
            playerlife = Content.Load<Texture2D>("playerliferec");
            font = Content.Load<SpriteFont>("myfont");
            shottex = Content.Load<Texture2D>("shottex");
            explosion = Content.Load<Texture2D>("explosion_pro");

            player = new Player(new Vector2(10, (BaseGame.screenHeight / 2) - (playertex.Height / 2)), playertex, playerlife, shottex, explosion); //Content.Load<Texture2D>("shottex")
            enemies = new Enemy[maxEnemies];
            enemiesSin = new SinEnemy[maxEnemiesSin];

            for (int i = 0; i < maxEnemiesSin; i++) enemiesSin[i] = new SinEnemy(new Vector2(random.Next(BaseGame.screenWidth, BaseGame.screenWidth + 2000), random.Next(0, BaseGame.screenHeight - enemytex.Height)), enemytex, playerlife, explosion);

            for (int i = 0; i < maxEnemies; i++) enemies[i] = new Enemy(new Vector2(random.Next(BaseGame.screenWidth, BaseGame.screenWidth + 2000), random.Next(0, BaseGame.screenHeight - enemytex.Height)), enemytex, playerlife, explosion);

            if (File.Exists("hiscore.txt")) hiScore = Convert.ToInt32(File.ReadAllText("hiscore.txt"));

        }

        public void Update(KeyboardState keyState)
        {
            framesCounter++;
            
            for (int i = 0; i < maxEnemies; i++)
            {
                if (enemies[i].Bounds.Intersects(player.Bounds) && enemies[i].Active)
                {
                    player.playerLifeWidth -= 25;
                    //tocar esto para que no choque mas de una vez.

                    enemies[i].Active = false;
                    enemies[i].Explode();
                }

                if (enemies[i].Active && player.CheckShotCollision(enemies[i].Bounds)) 
                {
                    enemies[i].enemyLifeWidth -= 50;
                    if (score > hiScore) hiScore = score;
                    if (enemies[i].enemyLifeWidth <= 0)
                    {
                        deadEnemiesCounter += 1;
                        enemies[i].Explode();
                        score += 50;
                    }
                }
                
                enemies[i].Update();
        
            }
            
            for (int i = 0; i < maxEnemiesSin; i++)
            {
                if (enemiesSin[i].Bounds.Intersects(player.Bounds) && enemies[i].Active)
                {
                    player.playerLifeWidth -= 25;
                    //tocar esto para que no choque mas de una vez.

                    enemiesSin[i].Active = false;
                    enemiesSin[i].Explode();
                }

                if (enemiesSin[i].Active && player.CheckShotCollision(enemiesSin[i].Bounds))
                {
                    enemiesSin[i].enemyLifeWidth -= 25;
                    if (score > hiScore) hiScore = score;
                    if (enemiesSin[i].enemyLifeWidth <= 0) 
                    {
                        deadEnemiesCounter += 2;
                        score += 100;
                        enemies[i].Explode(); 
                    }
                }

                enemiesSin[i].Update();

            }

            player.Update(keyState);

            if ((framesCounter >= 60) && (secondsCounter != 0))
            {
                framesCounter = 0;
                secondsCounter--;
            }

            if (deadEnemiesCounter >= 15)
            {
                deadEnemiesCounter = 0;
                secondsCounter += 5;
            }

            if (player.playerLifeWidth <= 0 || secondsCounter <= 0) player.Active = false;

            if (!player.Active)
            {
                if (score > hiScore) hiScore = score;
                
                playerDeadCounter++;
                player.Explode();
                if (playerDeadCounter > 200)
                {
                    score = 0;

                    player.Reset();//player = new Player(new Vector2(10, (BaseGame.screenHeight / 2) - (playertex.Height / 2)), playertex, playerlife, shottex, );

                    for (int i = 0; i < maxEnemiesSin; i++) enemiesSin[i].Reset(); //enemiesSin[i] = new SinEnemy(new Vector2(random.Next(BaseGame.screenWidth, BaseGame.screenWidth + 2000), random.Next(0, BaseGame.screenHeight - enemytex.Height)), enemytex, playerlife, explosion);

                    for (int i = 0; i < maxEnemies; i++) enemies[i].Reset(); //enemies[i] = new Enemy(new Vector2(random.Next(BaseGame.screenWidth, BaseGame.screenWidth + 2000), random.Next(0, BaseGame.screenHeight - enemytex.Height)), enemytex, playerlife, explosion);
                    secondsCounter = 99;
                    playerDeadCounter = 0;

                    File.WriteAllText("hiscore.txt", hiScore.ToString());

                    player.Active = true;

                    BaseGame.gameScreen = GameScreen.ENDING;
                }
                //Reiniciar variables aquí.

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(espacio, Vector2.Zero, Color.Red);
            for (int i = 0; i < maxEnemies; i++) enemies[i].Draw(spriteBatch);
            for (int i = 0; i < maxEnemiesSin; i++) enemiesSin[i].Draw(spriteBatch);
            spriteBatch.DrawString(font, secondsCounter.ToString("00"), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "Score: " + score.ToString("00"), new Vector2(100, 10), Color.White);
            spriteBatch.DrawString(font, "Hi-Score: " + hiScore.ToString("00"), new Vector2(300, 10), Color.White);
            spriteBatch.DrawString(font, "Bullets: " + player.currentDisparos.ToString("00"), new Vector2(600, 10), Color.White);
            player.Draw(spriteBatch);

        }
    }
}
