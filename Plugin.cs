using System;
using BepInEx;
using System.IO;
using HarmonyLib;
using BepInEx.Logging;
using System.Collections.Generic;
using SpritesheetAPI.JSONData;
using DiskCardGame;
using InscryptionAPI.Card;
using SpritesheetAPI.PortraitBehaviour;
using SpritesheetAPI.ParseImage;

namespace SpritesheetAPI
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("MADH.inscryption.JSONLoader", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {   
        public const string PluginGuid = "kel.inscryption.spritesheetapi";
        public const string PluginName = "Spritesheet API";
        public const string PluginVersion = "0.1.2";

        internal static ManualLogSource myLogger; // Log source.

        // Register Appearance Behaviour
        public readonly static CardAppearanceBehaviour.Appearance AP = CardAppearanceBehaviourManager.Add(PluginGuid, "Spritesheet Animator", typeof(SpritesheetBehaviour)).Id;

        private void Awake() {

            myLogger = Logger; // Make log source.

            Logger.LogInfo($"Loading {PluginName}...");

            // Parsing JSON data
            string[] files = JSONParser.FindJSONFiles();
            List<PortraitJSON> cardData = new List<PortraitJSON>();

            foreach(string file in files)
            {
                try
                {
                    PortraitJSON p = JSONParser.ParseToJSON(file);
                    cardData.Add(p);
                }
                catch (Exception)
                {
                    string name = Path.GetFileName(file);
                    myLogger.LogError($"Couldn't parse JSON data from file {name}!");
                    continue;
                }
            }

            // Parse JSON objects and add data to the 'CardsToPatch' + 'Spritesheets' lists
            foreach(PortraitJSON p in cardData)
            {
                bool flag1 = p.Spritesheet.IsNullOrWhiteSpace();
                bool flag2 = p.CardName.IsNullOrWhiteSpace();
                bool flag3 = p.FrameRate <= 0 || p.FrameRate > 60;
                bool flag4 = p.FrameCount <= 0;

                Action<string> InvalidData = (x) => myLogger.LogError($"Couldn't load spritesheet info for card {p.CardName ?? "(null)"}: {x}");

                if (flag1) InvalidData("Image filename isn't valid.");
                if (flag2) InvalidData("Card name isn't valid.");
                if (flag3) InvalidData("Frame rate should be greater than 0 and no more than 60.");
                if (flag4) InvalidData("Frame count should be greater than 0.");

                if (flag1 || flag2 || flag3 || flag4) continue;

                CardInfo card = null;
                Action CardNotFound = () => myLogger.LogError($"Couldn't load card by name: {p.CardName}");

                try
                {
                    card = CardLoader.GetCardByName(p.CardName);
                }
                catch (Exception)
                {
                    CardNotFound();
                    continue;
                }

                if(card == null)
                {
                    CardNotFound();
                    continue;
                }

                // If iteration has gotten this far, card was found.
                card.AddAppearances(AP);

                if (!Library.CardsToPatch.Contains(p.CardName))
                {
                    Library.CardsToPatch.Add(p.CardName);
                }
                if (!Library.Spritesheets.ContainsKey(p.CardName))
                {
                    Library.Spritesheets.Add(p.CardName, p);
                }
            }

            // Parsing spritesheets.
            ParseSpritesheet.Parse();
            
            // Harmony harmony = new Harmony("kel.harmony.dummyname");
            // harmony.PatchAll();

            myLogger.LogInfo($"{PluginName} loaded successfully!");
        }
    }
}
