using Newtonsoft.Json;
using System.IO;
using BepInEx;

namespace SpritesheetAPI.JSONData
{
    internal class JSONParser
    {
        public static string[] FindJSONFiles()
        {
            return Directory.GetFiles(Paths.PluginPath, "*_anim.json", SearchOption.AllDirectories);
        }

        public static PortraitJSON ParseToJSON(string path)
        {
            string data = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PortraitJSON>(data);
        }
    }
}
