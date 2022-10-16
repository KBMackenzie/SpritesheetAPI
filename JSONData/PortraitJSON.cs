using System;

namespace SpritesheetAPI.JSONData
{
    public class PortraitJSON
    {
        public string CardName;
        public string Spritesheet;
        public int FrameRate;
        public int FrameCount;
        public bool HasPause;
        public float PauseTime;

        public PortraitJSON(string cardName, string spritesheet, int frameRate, int frameCount, bool hasPause = false, float pauseTime = 0f)
        {
            CardName = cardName;
            Spritesheet = spritesheet;
            FrameRate = frameRate;
            FrameCount = frameCount;
            HasPause = hasPause;
            PauseTime = pauseTime;
        }
    }
}
