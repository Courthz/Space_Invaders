using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace BaseGame
{
    class Animation
    {
        public Rectangle CurrentRec { get; set; }

        public bool Finish { get; set; }

        int numFramesPerLine;
        int numLines;
        int frameWidth;
        int frameHeight;

        int framesCounter;
        int currentFrame;
        int currentLine;
        int totalDuration;

        bool loop = false;

        public Animation(Texture2D tex, int framesLine, int lines, int duration, bool isloop)
        {
            numFramesPerLine = framesLine;
            numLines = lines;
            frameWidth = tex.Width / numFramesPerLine;
            frameHeight = tex.Height / numLines;

            CurrentRec = new Rectangle (0, 0, frameWidth, frameHeight);

            totalDuration = (int)(duration / (numFramesPerLine * numLines));

            framesCounter = 0;
            currentFrame = 0;
            currentLine = 0;

            Finish = false;

            loop = isloop;
        }

        public void Update()
        {
            if (!Finish)
            {
                framesCounter++;

                if (framesCounter > totalDuration)
                {
                    currentFrame++;

                    if (currentFrame >= numFramesPerLine)
                    {
                        currentFrame = 0;
                        currentLine++;

                        if (currentLine >= numLines)
                        {
                            currentLine = 0;
                            if (!loop) Finish = true;
                        }
                    }

                    framesCounter = 0;
                }

                CurrentRec = new Rectangle(frameWidth * currentFrame, frameHeight * currentLine, frameWidth, frameHeight);
            }
        }

        public void Reset()
        {
            framesCounter = 0;
            currentFrame = 0;
            currentLine = 0;
            Finish = false;
        }
    }
}
