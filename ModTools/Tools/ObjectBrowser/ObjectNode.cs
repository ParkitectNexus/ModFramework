using UnityEngine;

namespace ModTools.Tools.ObjectBrowser
{
    class ObjectNode
    {
        public GameObject GameObject { get; set; }

        public bool Open { get; private set; }

        public ObjectNode(GameObject gameObject)
        {
            GameObject = gameObject;
        }
        public void SetOpen()
        {
            Open = true;
        }

        public void SetClosed()
        {
            Open = false;
        }

        public void Toggle()
        {
            Open = !Open;
        }
    }
}
