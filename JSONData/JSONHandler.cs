using System;

namespace SpritesheetAPI.JSONData
{
    public class PortraitJSON
    {
        public string CardName;
        public string Spritesheet;
        public int FrameRate;
        public int FrameCount;

        public PortraitJSON(string cardName, string spritesheet, int frameRate, int frameCount)
        {
            CardName = cardName;
            Spritesheet = spritesheet;
            FrameRate = frameRate;
            FrameCount = frameCount;
        }
    }
}
