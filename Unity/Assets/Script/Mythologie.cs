using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Mythologie : MonoBehaviour
    {
        public string _name;
        
        public List<GameObject> _monsters;

        public void Setup(string name, List<string> monsters)
        {
            _name = name;
            List<GameObject> prefabs = new List<GameObject>();
            
            foreach (var monster in monsters)
            {
                List<GameObject> unit = _monsters.FindAll(x => x.CompareTag(monster));
                foreach (var prefab in unit)
                {
                    prefabs.Add(prefab);
                }
            }

            _monsters = prefabs;
            Debug.Log("mise en place de la mythologie : " + _name);
        }
    }
}