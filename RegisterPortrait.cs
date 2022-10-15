using SpritesheetAPI.JSONData;
using SpritesheetAPI.ParseImage;
using SpritesheetAPI.PortraitBehaviour;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpritesheetAPI
{
    public static class RegisterPortrait
    {
        public static void Add(string CardName, string Spritesheet, int FrameRate, int FrameCount)
        {
            PortraitJSON p = new PortraitJSON(CardName, Spritesheet, FrameRate, FrameCount);
            Library.CardsToPatch.Add(CardName);
            Library.Spritesheets.Add(CardName, p);
            ParseSpritesheet.Parse();
        }
    }
}
