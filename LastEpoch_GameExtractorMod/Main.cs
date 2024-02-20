namespace LastEpoch_GameExtractorMod
{
    public class Main : MelonLoader.MelonMod
    {
        internal static MelonLoader.MelonLogger.Instance logger_instance = null;
        internal static string path = System.IO.Directory.GetCurrentDirectory() + @"\Mods\LastEpochGameExtractor\";

        public override void OnInitializeMelon()
        {
            logger_instance = LoggerInstance;
        }        
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Scenes.CurrentName = sceneName;
        }
        public override void OnLateUpdate()
        {
            if (!Universe_Lib.UniverseLibLoaded)
            {
                if (UniverseLib.Universe.CurrentGlobalState == UniverseLib.Universe.GlobalState.SetupCompleted)
                {
                    Universe_Lib.UniverseLibLoaded = true;
                }
                else if (UniverseLib.Universe.CurrentGlobalState == UniverseLib.Universe.GlobalState.WaitingToSetup)
                {
                    Universe_Lib.Init();
                }
            }
            else if (Scenes.GameScene())
            {
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F4))
                {
                    Data.Classes.Get();
                    Data.Items.Get();
                }
            }
        }
    }
}
