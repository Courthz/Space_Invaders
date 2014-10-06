using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;

namespace BaseGame
{
    static class Utils
    {
        public static Random random;

        // Call this function in BaseGame LoadContent and pass white-pixel-texture
        public static void Initialize()
        {
            random = new Random();
        }

        // Use this function wherever you want, just call Utils.DrawBounds(...)
        /*public static void DrawBounds(SpriteBatch sb, Rectangle bounds)
        {
            sb.Draw(texture, bounds, new Color(255, 0, 0, 40));
        }*/
    }
}
