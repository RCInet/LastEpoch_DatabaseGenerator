using UnityEngine;
using UniverseLib;

namespace LastEpoch_GameExtractorMod
{
    public class Functions
    {
        
        //Path
        internal static void VerifyDirectory(string path, string filename)
        {
            if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
            if (System.IO.File.Exists(path + filename)) { System.IO.File.Delete(path + filename); }
        }
        //Objects
        public static Object GetObject(string name)
        {
            Object objet = new Object();
            foreach (Object obj in RuntimeHelper.FindObjectsOfTypeAll(typeof(Object)))
            {
                if ((name != "") && (obj.name.Contains(name)))
                {
                    System.Type type = obj.GetActualType();
                    if (type != typeof(TextAsset))
                    {
                        objet = obj;
                        break;
                    }
                }
            }
            return objet;
        }
        internal static void SaveObject(Object obj, string name)
        {
            string path = Main.path + @"Objects\";
            string jsonString = UnityEngine.JsonUtility.ToJson(obj, true);
            string filename = obj.name + ".json";
            if (name != "") { filename = name + ".json"; }
            VerifyDirectory(path, filename);
            Main.logger_instance.Msg("Extracting " + obj.name + " to " + path + filename);
            System.IO.File.WriteAllText(path + filename, jsonString);
        }
    }
}
