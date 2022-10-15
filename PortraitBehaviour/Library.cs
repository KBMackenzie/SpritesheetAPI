using System.Collections.Generic;
using UnityEngine;
using SpritesheetAPI.JSONData;

namespace SpritesheetAPI.PortraitBehaviour
{
    internal static class Library
    {
        // List of card names to be iterated through
        public static List<string> CardsToPatch = new List<string>();

        // Look up card name and gets its PortraitJSON
        public static Dictionary<string, PortraitJSON> Spritesheets = new Dictionary<string, PortraitJSON>();

        // Animated portrait frames stored with card name as their key
        public static Dictionary<string, (Sprite[] frames, int frameRate)> AnimatedPortraits = new Dictionary<string, (Sprite[] frames, int frameRate)>();
    }
}
