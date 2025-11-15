using UnityEngine;

namespace Silt.Update
{
    public static class UpdateSystem
    {
        static UpdateSystem()
        {

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Init()
        {

        }

        private static readonly UpdateLoop _updateLoop = new();
    }
}