using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModTools.Tools
{
    class ObjectBrowser : MonoBehaviour
    {
        readonly Rect titleBarRect = new Rect(0, 0, 10000, 20);
        Rect windowRect = new Rect(20, 20, Screen.width - (20 * 2), Screen.height - (20 * 2));

        void OnGui()
        {
            windowRect = GUILayout.Window(123457, windowRect, DrawConsoleWindow, "Game Object Browser");
        }

        /// <summary>
        /// Displays a window that lists the recorded logs.
        /// </summary>
        /// <param name="windowID">Window ID.</param>
        void DrawConsoleWindow(int windowID)
        {
            //DrawLogsList();
            //DrawToolbar();

            // Allow the window to be dragged by its title bar.
            GUI.DragWindow(titleBarRect);
        }
    }
}
