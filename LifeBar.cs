using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;

namespace BaseGame
{
    class LifeBar
    {
        Vector2 position;
        Texture2D texture;
        Color color;

        int maxSize;
        int currentSize;

        public LifeBar(Texture2D tex, int maxSize)
        {
            position = Vector2.Zero;
            texture = tex;
            color = Color.White;

            this.maxSize = maxSize;
            currentSize = maxSize;
        }

        public void Update(Vector2 pos, int size)
        {
            position = pos + new Vector2();

            currentSize = size;

            if (currentSize < 0) currentSize = 0;
            //Vector2.Lerp. . .
            //movimiento sinusoidal--> posy = A * Math.Sin
        }

        public void Draw (SpriteBatch sb)
        {
            sb.Draw(texture, new RectangleF(position.X, position.Y, currentSize, 40), color);
            sb.Draw(texture, new RectangleF(position.X, position.Y, maxSize, 40), Color.Red);
            sb.Draw(texture, new RectangleF(position.X - 5, position.Y - 5, maxSize + 10, 40), Color.Black);
            
        }
    }
}
