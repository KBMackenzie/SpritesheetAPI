using System;
using System.Linq;
using System.Collections.Generic;
using SpritesheetAPI.PortraitBehaviour;
using HarmonyLib;
using SpritesheetAPI.JSONData;
using UnityEngine;
using System.IO;
using BepInEx;

namespace SpritesheetAPI.ParseImage
{
    internal static class ParseSpritesheet
    {
        // Portrait size
        public const int pWidth = 114, pHeight = 94;

        public static void Parse()
        {
            // Message Log
            Action<string> LogInfo  = (x) => Plugin.myLogger.LogInfo(x);
            Action<string> LogError = (x) => Plugin.myLogger.LogError(x);

            List<string> CardsToPatch = Library.CardsToPatch;

            foreach(string Card in CardsToPatch)
            {
                try
                {
                    // See if Card has already been parsed to (Sprite[], float).
                    if (Library.AnimatedPortraits.ContainsKey(Card)) continue;

                    if (!Library.Spritesheets.ContainsKey(Card)) continue;
                    PortraitJSON Data = Library.Spritesheets[Card];

                    string path = FindImage(Data.Spritesheet);
                    if (path == null) continue;

                    Texture2D tex = LoadTexture2D(path);
                    Sprite[] frames = LoadAllSprites(tex, Data.FrameCount);

                    // Set 'PauseTime' to 0 if conditions are true
                    if (!Data.HasPause || Data.PauseTime < 0) Data.PauseTime = 0f;   

                    Library.AnimatedPortraits.Add(Card, (frames, Data.FrameRate, Data.PauseTime));
                    LogInfo($"Loaded animated portrait data for card \"{Card}\"!");
                }
                catch (Exception)
                {
                    LogError($"Error loading spritesheet data for card \"{Card}\"!");
                }
            }
        }

        public static Texture2D LoadTexture2D(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(1, 1);
            ImageConversion.LoadImage(tex, data);
            return tex;
        }

        public static Sprite MakeSprite(Texture2D tex)
        {
            Rect texRect = new Rect(0, 0, tex.width, tex.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(tex, texRect, pivot);
        }

        public static Sprite[] LoadAllSprites(Texture2D tex, int frameCount)
        {
            int imgHeight = tex.height;
            int imgWidth = tex.width;
            int rows = imgHeight / pHeight;
            int cols = imgWidth / pWidth;

            List<Sprite> Output = new List<Sprite>();

            // Bottom left-corner of image is (0, 0) for some reason. Work around that.

            for(int y = rows-1; y >= 0; y--)
            {
                for(int x = 0; x < cols; x++)
                {
                    Rect texRect = new Rect(x*pWidth, y*pHeight, pWidth, pHeight);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    Sprite spr = Sprite.Create(tex, texRect, pivot);

                    Output.Add(spr);

                    // Make sure not to make sprites from blank frames:
                    if (Output.Count >= frameCount) { break; }
                }

                // In case the image has more columns than sprites.
                if (Output.Count == frameCount) { break; }
            }

            return Output.ToArray();
        }

        public static string FindImage(string fileName)
        {
            string[] files = Directory.GetFiles(Paths.PluginPath, fileName, SearchOption.AllDirectories);

            // Message Log
            Action<string> LogError = (x) => Plugin.myLogger.LogError($"FindImage: {x}");
            Action<string> LogWarning = (x) => Plugin.myLogger.LogInfo($"FindImage: {x}");

            // Error-Logging
            if (files.Length == 0)
            {
                LogError($"Couldn't find image \"{fileName}\" in the \'plugins\' folder.");
                return null;
            }
            else if(files.Length > 1)
            {
                LogWarning($"Found more than one file named \"{fileName}\" in the \'plugins\' folder. This could lead to the wrong file being loaded.");
            }

            return files.First();
        }
    }
}
