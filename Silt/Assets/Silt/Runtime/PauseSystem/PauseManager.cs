using UnityEngine;
using UnityEngine.SceneManagement;

namespace Silt.Pause
{
    internal static class PauseManager
    {
        public readonly static PauseLight<PauseReason> _system = new();
        static PauseManager()
        {
            Application.quitting += _system.Clear;
            SceneManager.sceneLoaded += (_, mode) =>
            {
                if (mode != LoadSceneMode.Single)
                    return;
                _system.Clear();
            };
        }
    }
}