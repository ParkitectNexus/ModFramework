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
        /// The additional hotkey to set toggle activation status of settings mode.
        /// </summary>
        public KeyCode activationToggleKey = KeyCode.LeftControl;

        /// <summary>
        /// Settings mode is active status.
        /// </summary>
        public bool isActive = false;

        /// <summary>
        /// Show console? Can be toggled in settings gui.
        /// </summary>
        public bool showConsole = true;

        /// <summary>
        /// Show objectbrowser? Can be toggled in settings gui.
        /// </summary>
        public bool showObjectBrowser = true;

        /// <summary>
        /// Show pressed key codes as debug message.
        /// </summary>
        public bool debugKeyCode = false;

        /// <summary>
        /// Show global settings window.
        /// </summary>
        private bool _showWindow = false;

        /// <summary>
        /// Settings window rect.
        /// </summary>
        private Rect _window = new Rect(50, 50, 1, 1);

        #endregion

        void Start()
        {
            // set window to center of screen with default size
            const int windowHeight = 140;
            const int windowWidth = 320;
            _window = new Rect(
                Screen.width / 2 - windowWidth / 2, 
                Screen.height / 2 - windowHeight / 2, 
                windowWidth, 
                windowHeight
           );
        }

        void Update()
        {
            if (Input.GetKeyDown(activationToggleKey)) {
                isActive = true;
            } else if (Input.GetKeyUp(activationToggleKey)) {
                isActive = false;
            }

            if (isActive == true && (Input.GetKeyDown(toggleKey) || Input.GetKeyDown(toggleKeyDE)))
            {
                Debug.Log(234234);
                _showWindow = !_showWindow;
            }
        }

        private Rect _rect(int index)
        {
            const int height = 20;
            const int margin = 5;

            return new Rect(5, 20 + (height + margin) * index, 300, height);
        }

        private void _doWindow(int id)
        {
            var index = 0;
            showConsole = GUI.Toggle(_rect(index++), showConsole, "Show Console");

            debugKeyCode = GUI.Toggle(_rect(index++), debugKeyCode, "Debug Key Code");

            showObjectBrowser = GUI.Toggle(_rect(index++), showObjectBrowser, "Show Object Browser");

            if (GUI.Button(_rect(index++), "Close")) _showWindow = false;

            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }

        private void OnGUI()
        {
            if (!_showWindow)
                return;

            _window = GUI.Window(0, _window, _doWindow, "ModTools Settings");
        }
    }
}
