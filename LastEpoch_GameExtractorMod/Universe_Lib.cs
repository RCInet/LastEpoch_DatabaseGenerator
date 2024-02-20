namespace LastEpoch_GameExtractorMod
{
    internal class Universe_Lib
    {
        internal static bool UniverseLibLoaded = false;
        internal static void Init()
        {
            UniverseLib.Universe.Init(5f, UniverseLib_OnInitialized, UniverseLib_LogHandler, new UniverseLib.Config.UniverseLibConfig()
            {
                Disable_EventSystem_Override = false,
                Force_Unlock_Mouse = true,
                Unhollowed_Modules_Folder = System.IO.Directory.GetCurrentDirectory() + @"\MelonLoader\"
            });
        }
        internal static void UniverseLib_OnInitialized()
        {
            UniverseLibLoaded = true;
            Main.logger_instance.Msg("UniverseLib init completed");
        }
        internal static void UniverseLib_LogHandler(string message, UnityEngine.LogType type)
        {
            Main.logger_instance.Msg("UniverseLib : " + message);
        }
    }
}
