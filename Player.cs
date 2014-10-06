using SharpDX;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace BaseGame
{
    class Player
    {
        Vector2 position;
        int speed;

        Texture2D texture, lifetexture, shottex, explosionTex;
        Rectangle playerLife, playerLifeRed;
        public Rectangle Bounds {get; set;}
        public bool Active { get; set; }

        public int playerLifeWidth = 100;

        Shot[] disparos;
        const int maxDisparos = 3;
        int shotCounter = 0;
        public int currentDisparos = maxDisparos;

        Explosion explosion;

        public Player(Vector2 position, Texture2D tex, Texture2D lifetex, Texture2D texShot, Texture2D texExplosion)
        {
            //TODO: Initialize all properties
            this.position = position;
            speed = 5;
            texture = tex;
            lifetexture = lifetex;
            shottex = texShot;
            explosionTex = texExplosion;

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            playerLife = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), playerLifeWidth, 10);
            playerLifeRed = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), 100, 10);
            Active = true;

            disparos = new Shot[maxDisparos];

            for (int i = 0; i < maxDisparos; i++) disparos[i] = new Shot(Vector2.Zero, new Vector2(8, 0), texShot);

            explosion = new Explosion(texExplosion, 8, 6, 96);
        }

        public void Update(KeyboardState keyState)
        {
            if (Active)
            {
            //TODO: Update everything that requires updating      
                if (keyState.IsKeyDown(Keys.Up)) position.Y -= speed;
                if (keyState.IsKeyDown(Keys.Down)) position.Y += speed;
                if (keyState.IsKeyDown(Keys.Right)) position.X += speed;
                if (keyState.IsKeyDown(Keys.Left)) position.X -= speed;

                if (position.X <= 0) position.X = 0;
                if ((position.X + texture.Width)>= BaseGame.screenWidth) position.X = BaseGame.screenWidth - texture.Width;
                if (position.Y <= 0) position.Y = 0;
                if ((position.Y + texture.Height)>= BaseGame.screenHeight) position.Y = BaseGame.screenHeight - texture.Height;

                Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                playerLife = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), playerLifeWidth, 10);
                playerLifeRed = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), 100, 10);
                for (int i = 0; i < maxDisparos; i++)
                    if (disparos[i].Bounds.BottomLeft.X > BaseGame.screenWidth && disparos[i].Active == true)
                    {
                        disparos[i].Active = false;
                        currentDisparos += 1;
                    }

                shotCounter++;

                if (shotCounter >= 20)
                {
                    if (keyState.IsKeyDown(Keys.Space))
                    {
                        for (int i = 0; i < maxDisparos; i++)
                        {
                            if (!disparos[i].Active)
                            {
                                shotCounter = 0;
                                disparos[i].FireShot(position + new Vector2(texture.Width / 2 + 20, (texture.Height / 2) - (shottex.Height / 2)));
                                i = maxDisparos;
                                currentDisparos -= 1;
                            }
                        }
                    }
                }

                /*if (keyState.IsKeyPressed(Keys.R))      //Intento de hacer un reload para recargar balas peeero he tenido un problema (balas que seguian activas y dentro de la pantalla se reseteaban tambien.)
                {
                    for (int i = 0; i < maxDisparos; i++)
                    {
                        if (disparos[i].Active = true && disparos[i].Bounds.BottomLeft.X > BaseGame.screenWidth)
                        {
                            disparos[i].Active = false;
                            currentDisparos = maxDisparos;
                        }
                    }
                }*/
            }

            if (!Active) Bounds = new Rectangle(0,0,0,0);

            for (int i = 0; i < maxDisparos; i++) disparos[i].Update();

            explosion.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Draw one shot
            if (Active) 
            {
                spriteBatch.Draw(texture, new Vector2((int)position.X, (int)position.Y), Color.White);
                spriteBatch.Draw(lifetexture, playerLifeRed, Color.Red);
                spriteBatch.Draw(lifetexture, playerLife, Color.Green);
            }

            for (int i = 0; i < maxDisparos; i++) disparos[i].Draw(spriteBatch);

            explosion.Draw(spriteBatch);
        }

        //TODO: Extra functions required? Probably yes...

        public bool CheckShotCollision(Rectangle enemyBounds)
        {
            bool collision = false;

            for (int i = 0; i < maxDisparos; i++)
            {
                if (disparos[i].Active && disparos[i].Bounds.Intersects(enemyBounds) && disparos[i].Bounds.BottomLeft.X <= BaseGame.screenWidth)
                {
                    currentDisparos += 1;
                    disparos[i].Active = false;
                    collision = true;
                }
            }

            return collision;
        }

        public void Explode()
        {
            explosion.Explode(position + new Vector2(texture.Width / 2, texture.Height / 2));
            
        }

        public void Reset()
        {
            //hola, asme.
            position = new Vector2(10, (BaseGame.screenHeight / 2) - (texture.Height / 2));
            playerLifeWidth = 100;
            currentDisparos = 3;

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            playerLife = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), playerLifeWidth, 10);
            playerLifeRed = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), 100, 10);

            disparos = new Shot[maxDisparos];
            for (int i = 0; i < maxDisparos; i++) disparos[i] = new Shot(Vector2.Zero, new Vector2(8, 0), shottex);
        }
    }
}
