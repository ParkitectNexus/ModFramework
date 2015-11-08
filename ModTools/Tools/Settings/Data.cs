﻿using System.Collections.Generic;
using MiniJSON;
using System;

namespace ModTools.Tools.Settings
{
    class Data
    {
        protected FilesHandler fh = new FilesHandler();
        protected List<string> handles = new List<string>();

        public bool errorOnLoad = false;
        public bool errorOnSave = false;

        // implementation for global data
        public Settings.Global settings;

        protected virtual Dictionary<string, object> getDict(string handle)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            // implementation for global (handle) data
            dict.Add("showConsole", settings.showConsole);
            dict.Add("showObjectBrowser", settings.showObjectBrowser);
            dict.Add("debugKeyCode", settings.debugKeyCode);

            return dict;
        }

        protected virtual bool setByDictKey(Dictionary<string, object> dict, string key)
        {
            // implementation for global (handle) data
            switch (key)
            {
                case "showConsole":
                    settings.showConsole = Convert.ToBoolean(dict[key]);
                    break;
                case "showObjectBrowser":
                    settings.showObjectBrowser = Convert.ToBoolean(dict[key]);
                    break;
                case "debugKeyCode":
                    settings.debugKeyCode = Convert.ToBoolean(dict[key]);
                    break;
            }

            return true;
        }

        protected virtual bool setByDict(Dictionary<string, object> dict)
        {
            bool success = true;
            foreach (string key in dict.Keys)
            {
                success = success && setByDictKey(dict, key);
            }
            return success;
        }

        public void addHandle(string handle)
        {
            if (!handles.Contains(handle))
            {
                handles.Add(handle);
                fh.add(handle);
            }
        }

        public virtual bool updateByHandles(string mode = "set")
        {
            bool success = true;
            foreach (string handle in handles)
            {
                if (mode == "set")
                {
                    Dictionary<string, object> dict = getDict(handle);
                    success = success && (dict != null);
                    if (success) success = success && fh.set(handle, Json.Serialize(getDict(handle)));
                }
                else
                {
                    string content = fh.get(handle);
                    success = success && (content != null);
                    if (success) success = success && setByDict(Json.Deserialize(content) as Dictionary<string, object>);
                }
            }
            return success;
        }

        public virtual List<string> saveByHandles()
        {
            errorOnSave = false;
            errorOnSave = !updateByHandles("set");
            List<string> messages = new List<string>();
            if (!errorOnSave) {
                messages = fh.saveAll();
                errorOnSave = fh.errorOnSave;
            }
            if (errorOnSave) messages.Add("Error on save handles"); 
            return messages;
        }

        public virtual List<string> loadByHandles()
        {
            errorOnLoad = false;
            List<string> messages = fh.loadAll();
            errorOnLoad = fh.errorOnLoad;
            if (!errorOnLoad && !updateByHandles("get")) errorOnLoad = true;
            if (errorOnLoad) messages.Add("Error on load handles"); 
            return messages;
        }
    }
}
