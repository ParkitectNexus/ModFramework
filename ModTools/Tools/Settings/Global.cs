using UnityEngine;

namespace ModTools.Tools.Settings
{
    class Global : MonoBehaviour
    {

        #region Inspector Settings

        /// <summary>
        /// The primary hotkey to show and hide the tools.
        /// </summary>
        public KeyCode toggleKey = KeyCode.BackQuote;

        /// <summary>
        /// The secondary hotkey for germany to show and hide the tools.
        /// </summary>
        public KeyCode toggleKeyDE = KeyCode.Backslash;

        /// <summary>
        /// The additional hotkey to set toggle settings mode.
        /// </summary>
        public KeyCode shiftKey = KeyCode.LeftShift;

        /// <summary>
        /// Show console? Can be toggled in settings gui.
        /// </summary>
        public bool showConsole = true;

        /// <summary>
        /// Show objectbrowser? Can be toggled in settings gui.
        /// </summary>
        public bool showObjectBrowser = false;

        /// <summary>
        /// Show pressed key codes as debug message.
        /// </summary>
        public bool debugKeyCode = true;

        #endregion

        public void Start()
        {
            Debug.Log("Mod tools settings start");
        }

        public void onDisabled()
        {
            Debug.Log("destroyed settings");
        }
    }
}
