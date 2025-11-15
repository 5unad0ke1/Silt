using Silt.Core;
using UnityEngine;

namespace Silt.Runtime
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