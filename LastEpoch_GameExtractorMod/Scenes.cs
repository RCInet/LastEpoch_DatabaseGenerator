using System.Linq;

namespace LastEpoch_GameExtractorMod
{
    public class Scenes
    {
        public static string CurrentName = "";        
        public static bool GameScene()
        {
            if ((CurrentName != "") && (!MenuNames.Contains(CurrentName))) { return true; }
            else { return false; }
        }

        private static readonly string[] MenuNames = { "PersistentUI", "Login", "CharacterSelectScene" };
    }
}
