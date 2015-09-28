using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModTools.Tools.ObjectBrowser
{
    class GameObjectTree
    {
        public GameObject GameObject
        {
            get { return _gameObject; }
        }

        private readonly GameObject _gameObject;

        private List<GameObjectTree> children;

        public bool Open;

        public int Count
        {
            get { return children.Count; }
        }

        public GameObjectTree(GameObject go)
        {
            this._gameObject = go;

            children = new List<GameObjectTree>();
        }

        public GameObjectTree AddChild(GameObject go)
        {
            var tree = new GameObjectTree(go);

            children.Add(tree);

            return tree;
        }

        public GameObjectTree GetChild(int i)
        {
            return children[i];
        }

        public void RemoveChild(int i)
        {
            children.RemoveAt(i);
        }

        public IEnumerable<GameObjectTree> GetChildren()
        {
            return children;
        }
    }
}
