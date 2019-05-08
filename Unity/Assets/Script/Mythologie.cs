using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Mythologie : MonoBehaviour
    {
        public string _name;
        
        public List<GameObject> _monsters;

        public void Setup(string name, List<GameObject> monsters)
        {
            _name = name;
            _monsters = monsters;
            
            Debug.Log("mise en place de la mythologie : " + _name);
        }
    }
}