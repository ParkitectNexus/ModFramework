using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModTools.Tools.ObjectBrowser
{
    class ObjectBrowser : MonoBehaviour
    {
        private bool _visible = false;

        // UI stuff
        private readonly Rect _titleBarRect = new Rect(0, 0, 10000, 20);
        Vector2 _scrollPosition;

        Rect _windowRect = new Rect(20, 20, Screen.width / 1.66f - (20 * 2), Screen.height - (20 * 2));

        private bool _filterJunk = false;

        private readonly Filter _filter = new Filter();

        private GameObject _selected;

        private List<GameObjectTree> _gos = new List<GameObjectTree>(); 

        private List<GameObject> open = new List<GameObject>();

        private SettingsFull _settings = null;

        void Start()
        {
            Debug.Log("Mod tools browser start");
            //SceneRoots();
            StartCoroutine(WaitForSettingsToSet());
        }

        private IEnumerator WaitForSettingsToSet()
        {
            while (_settings == null)
            {
                _settings = FindObjectOfType<SettingsFull>();
                yield return new UnityEngine.WaitForSeconds(1);
            }
        }

        private void Update()
        {
            if (_settings != null)
            {
                if (Input.GetKeyDown(_settings.toggleKey) || Input.GetKeyDown(_settings.toggleKeyDE))
                {
                    _visible = (_settings.showObjectBrowser) ? !_visible : false;
                }
            }
        }
        
        void OnGUI()
        {
            if (_visible)
                _windowRect = GUILayout.Window(123457, _windowRect, DrawConsoleWindow, "Game Object Browser");
        }

        /// <summary>
        /// Displays a window that lists the recorded logs.
        /// </summary>
        /// <param name="windowID">Window ID.</param>
        void DrawConsoleWindow(int windowID)
        {
            DrawToolbar();
            DrawContents();
            // Allow the window to be dragged by its title bar.
            GUI.DragWindow(_titleBarRect);
        }

        private void DrawContents()
        {
            GUILayout.BeginHorizontal();
            DrawObjects();
            DrawViewer();
            GUILayout.EndHorizontal();
        }

        private void DrawObjects()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            foreach (GameObjectTree treeGo in SceneRoots())
            {
                DrawObject(treeGo, 0);
            }

            GUILayout.EndScrollView();
        }

        private void DrawObject(GameObjectTree treeGo, int spacing)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(spacing);

            if (!open.Contains(treeGo.GameObject))
            {
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    open.Add(treeGo.GameObject);
                }
            }
            else
            {
                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    open.Remove(treeGo.GameObject);
                }
            }

            if (GUILayout.Button(treeGo.GameObject.name, GUI.skin.label))
            {
                _selected = treeGo.GameObject;
            }

            GUILayout.EndHorizontal();

            if (open.Contains(treeGo.GameObject))
            {
                foreach (GameObjectTree childTreeGo in treeGo.GetChildren())
                {
                    DrawObject(childTreeGo, spacing + 10);
                }
            }
        }

        private void DrawViewer()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Delete"))
            {
                Destroy(_selected);
            }

            GUILayout.EndHorizontal();

            if (_selected != null)
            {
                foreach (Component component in _selected.GetComponents(typeof(Component)))
                {
                    GUILayout.Label(component.GetType().ToString());

                    foreach (PropertyInfo field in component.GetType().GetProperties())
                    {
                        if (field.CanRead && field.CanWrite)
                        {
                            GUILayout.Label(field.Name);

                            //GUILayout.Label((string)field.GetValue(component));

                            //if (field.PropertyType == typeof(string))
                            //{
                            //    
                            //}
                        }
                    }
                }
            }

            GUILayout.EndScrollView();
        }

        /// <summary>
        /// Displays options for filtering and changing the logs list.
        /// </summary>
        void DrawToolbar()
        {
            GUILayout.BeginHorizontal();

            _filterJunk = GUILayout.Toggle(_filterJunk, "Filter Junk GO's");

            GUILayout.EndHorizontal();
        }

        //public void UpdateGOs()
        //{
        //    Debug.Log("Updating");
        //    UnityEngine.GameObject[] objects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        //    List<GameObject> rootGos = FindRoots(objects);

        //    _gos.RemoveAll(t => !rootGos.Contains(t.GameObject));

        //    _gos.AddRange(FindNewGos(rootGos));

        //    foreach (GameObject gameObject in rootGos.Where(g => _gos.All(j => j.GameObject != g)))
        //    {
        //        _gos.AddRange(new GameObjectTree(gameObject));
        //    }
        //}

        //private List<GameObject> FindRoots(GameObject[] objects)
        //{
        //    List<GameObject> roots = new List<GameObject>();

        //    foreach (GameObject go in objects)
        //    {
        //        if (go != null && go.transform.parent)
        //        {
        //            roots.Add(go);
        //        }
        //    }

        //    return roots;
        //}

        public IEnumerable<GameObjectTree> SceneRoots()
        {

            UnityEngine.Object[] objects = FindObjectsOfType(typeof(GameObject));

            _gos.Clear();

            foreach (UnityEngine.Object o in objects)
            {
                GameObject go = o as GameObject;

                if (go != null && go.transform.parent == null)
                {
                    GameObjectTree tree = new GameObjectTree(go);

                    FillChildren(tree, go);

                    _gos.Add(tree);
                }
            }

            if (_filterJunk)
                return _filter.FilterJunk(_gos);

            return _gos;
        }

        private void FillChildren(GameObjectTree tree, GameObject go)
        {
            foreach (Transform transform in go.transform)
            {
                tree.AddChild(transform.gameObject);

                FillChildren(tree.GetChild(tree.Count - 1), transform.gameObject);
            }
        }
    }
}
