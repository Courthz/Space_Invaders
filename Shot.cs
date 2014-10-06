using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace BaseGame
{
    class Shot
    {
        Vector2 position;
        Vector2 speed;
        Color color;

        Texture2D texture;
        public Rectangle Bounds {get; private set;}

        public bool Active { get; set; }

        public Shot(Vector2 position, Vector2 speed, Texture2D tex)
        {
            //TODO: Initialize all properties
            texture = tex;
            this.position = Vector2.Zero;
            this.speed = speed;
            color = Color.Blue;

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            Active = false;
        }

        public void Update()
        {
            //TODO: Update everything that requires updating
            if (Active)
            {
                position += speed;

                Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

               
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Draw one shot
            if (Active)spriteBatch.Draw(texture, position, color);
        }

        //TODO: Extra functions required? Probably yes...
        public void FireShot(Vector2 initPos)
        {
            if (!Active) 
            {
                Active = true;
                position = initPos;
            }
        }
    }
}
