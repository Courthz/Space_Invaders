using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace BaseGame
{
    class Explosion
    {
        Vector2 position;
        Texture2D texture;
        Color color;
        bool active;

        Animation anim;

        public Explosion(Texture2D tex, int framesLine, int numLines, int duration)
        {
            //TODO: Initialize all properties
            texture = tex;
            position = Vector2.Zero;
            color = Color.White;
            active = false;

            anim = new Animation(texture, framesLine, numLines, duration, false);
        }

        public void Update()
        {
            if (active)
            {
                anim.Update();
                if (anim.Finish) active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Draw explosion
            if (active) spriteBatch.Draw(texture, position, anim.CurrentRec, color, 0, new Vector2(anim.CurrentRec.Width/2, anim.CurrentRec.Height/2), 1, SpriteEffects.None, 0);
        }

        public void Explode(Vector2 pos)
        {
            if (!active)
            {
                position = pos;
                active = true;
            }
        }
    }
}
