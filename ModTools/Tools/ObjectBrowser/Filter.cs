using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModTools.Tools.ObjectBrowser
{
    class Filter
    {
        private List<string> junk = new List<string>()
        {
            "VomitFootprint(Clone)",
            "foot_dust(Clone)"
        }; 

        public IEnumerable<GameObjectTree> FilterJunk(IEnumerable<GameObjectTree> gos)
        {
            foreach (GameObjectTree gameObjectTree in gos)
            {
                IEnumerable<GameObjectTree> children = gameObjectTree.GetChildren();

                for (int i = 0; i < children.Count(); i++)
                {
                    if (junk.Contains(children.ElementAt(i).GameObject.name))
                    {
                        gameObjectTree.RemoveChild(i);
                    }
                }
            }
            return gos;
            //foreach (Tree<GameObject> tree in gos)
            //{
            //    tree.Traverse(tree, Visitor);
            //    if (!junk.Contains(gameObject.name))
            //    {
            //        newGos.Add(gameObject);
            //    }
            //}

            //return newGos;
        }

        private void Visitor(GameObject nodedata)
        {
            
        }
    }
}
