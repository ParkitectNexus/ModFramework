using ModTools.Tools;
using UnityEngine;

namespace ModTools
{
    public class ModTools : IMod
    {
        private GameObject _go;

        public void onEnabled()
        {
            _go = new GameObject();

            _go.AddComponent<Console>();
        }

        public void onDisabled()
        {

        }

        public string Name { get { return "ModTools"; } }
        public string Description { get { return "Helpers for mods"; } }
    }
}
