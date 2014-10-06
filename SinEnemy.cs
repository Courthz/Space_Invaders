using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;

namespace BaseGame
{
    class SinEnemy
    {
        Vector2 position;
        int speed;
        public int deadCounter, yIni;
        float fase, amplitud;

        Texture2D texture, explosionTex, lifetexture;
        Rectangle enemyLife, enemyLifeRed;
        public Rectangle Bounds { get; private set; }

        public int enemyLifeWidth = 100;

        Explosion explosion;

        public bool Active { get; set; }

        //TODO: Extra properties required?

        public SinEnemy(Vector2 position, Texture2D tex, Texture2D lifetex, Texture2D texExplosion)
        {
            //TODO: Initialize all properties
            Utils.Initialize();
            deadCounter = 0;
            this.position = position;
            speed = Utils.random.Next(3, 6);
            fase = (float)(2 * Math.PI) / Utils.random.Next(300, 600);
            amplitud = Utils.random.Next(150, 300);
            lifetexture = lifetex;
            texture = tex;
            explosionTex = texExplosion;
            yIni = Utils.random.Next(0, BaseGame.screenHeight - texture.Height);

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            enemyLife = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), enemyLifeWidth, 10);
            enemyLifeRed = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), 20, 10);
            explosion = new Explosion(texExplosion, 8, 6, 96);

            Active = true;
        }

        public void Update()
        {
            if (enemyLifeWidth <= 0 || Bounds.BottomRight.X <= 0) Active = false;

            if (Active)
            {
                //TODO: Update everything that requires updating
                position.X -= speed;
                position.Y = yIni + amplitud * (float)Math.Sin(fase * position.X);
                Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                enemyLife = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), enemyLifeWidth, 10);
                enemyLifeRed = new Rectangle((int)(position.X - 20), (int)(position.Y - 20), 100, 10);
            }

            explosion.Update();

            if (!Active)
            {
                deadCounter++;
                Bounds = new Rectangle(0, 0, 0, 0);
                if (deadCounter >= 60) Reset();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                //TODO: Draw one shot
                spriteBatch.Draw(texture, new Vector2((int)position.X, (int)position.Y), Color.White);
                spriteBatch.Draw(lifetexture, enemyLifeRed, Color.Red);
                spriteBatch.Draw(lifetexture, enemyLife, Color.Green);
            }
            explosion.Draw(spriteBatch);
        }

        //TODO: Extra functions required? Probably yes...
        public void Reset()
        {
            //hola, asme!
            deadCounter = 0;
            enemyLifeWidth = 100;
            position.X = Utils.random.Next(BaseGame.screenWidth, BaseGame.screenWidth + 2000);
            yIni = Utils.random.Next(0, BaseGame.screenHeight - texture.Height);
            speed = Utils.random.Next(3, 6);
            fase = (float)(2 * Math.PI) / Utils.random.Next(300, 600);
            amplitud = Utils.random.Next(150, 300);

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            enemyLife = new Rectangle((int)(position.X + texture.Width / 2 - 20), (int)(position.Y - 20), enemyLifeWidth, 10);
            enemyLifeRed = new Rectangle((int)(position.X + texture.Width / 2 - 20), (int)(position.Y - 20), 20, 10);

            Active = true;
        }

        public void Explode()
        {
            if (Active)
            {
                explosion.Explode(position + new Vector2(texture.Width / 2 - explosionTex.Width / 2, texture.Height / 2 - explosionTex.Width / 2));
            }
        }
    }
}
