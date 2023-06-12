using MelonLoader;
using System.Linq;
using UnityEngine;

namespace LastEpoch_DatabaseGenerator
{
    public class Main : MelonMod
    {
        private bool UnityExplorerLoaded = false;
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Scenes.CurrentName = sceneName;
        }
        public override void OnLateUpdate()
        {
            if (!UnityExplorerLoaded) 
            {
                if (UnityExplorer.ObjectExplorer.SceneHandler.SelectedScene != null) { UnityExplorerLoaded = true; }                
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F11)) { Mods.LastEpochSaveEditor.GenerateDatabase(this); }                
            }
        }
    }
}
