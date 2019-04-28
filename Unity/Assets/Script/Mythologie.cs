using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Mythologie
    {
        private string _name;
        public string Name => _name;
        
        private List<GameObject> _monsters;
        public List<GameObject> Monsters => _monsters;
    }
}