using DiskCardGame;
using SpritesheetAPI.JSONData;
using SpritesheetAPI.ParseImage;
using SpritesheetAPI.PortraitBehaviour;
using System;
using BepInEx;
using InscryptionAPI.Card;

namespace SpritesheetAPI
{
    public static class RegisterPortrait
    {
        public static void Add(string CardName, string Spritesheet, int FrameRate, int FrameCount, bool HasPause = false, float PauseTime = 0f)
        {
            PortraitJSON p = new PortraitJSON(CardName, Spritesheet, FrameRate, FrameCount, HasPause, PauseTime);
            bool flag1 = p.Spritesheet.IsNullOrWhiteSpace();
            bool flag2 = p.CardName.IsNullOrWhiteSpace();
            bool flag3 = p.FrameRate <= 0 || p.FrameRate > 60;
            bool flag4 = p.FrameCount <= 0;

            Action<string> InvalidData = (x) => Plugin.myLogger.LogError($"Couldn't load spritesheet info for card {p.CardName ?? "(null)"}: {x}");

            if (flag1) InvalidData("Image filename isn't valid.");
            if (flag2) InvalidData("Card name isn't valid.");
            if (flag3) InvalidData("Frame rate should be greater than 0 and no more than 60.");
            if (flag4) InvalidData("Frame count should be greater than 0.");

            if (flag1 || flag2 || flag3 || flag4) return;

            CardInfo card = null;
            Action CardNotFound = () => Plugin.myLogger.LogError($"Couldn't load card by name: {p.CardName}");

            try
            {
                card = CardLoader.GetCardByName(p.CardName);
            }
            catch (Exception)
            {
                CardNotFound();
                return;
            }

            if (card == null)
            {
                CardNotFound();
                return;
            }

            // If iteration has gotten this far, card was found.
            card.AddAppearances(Plugin.AP);

            if (!Library.CardsToPatch.Contains(p.CardName))
            {
                Library.CardsToPatch.Add(p.CardName);
            }
            if (!Library.Spritesheets.ContainsKey(p.CardName))
            {
                Library.Spritesheets.Add(p.CardName, p);
            }

            ParseSpritesheet.Parse();
        }
    }
}
