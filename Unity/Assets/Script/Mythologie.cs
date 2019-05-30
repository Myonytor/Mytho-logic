using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Mythologie : MonoBehaviour
    {
        private string _name;
        
        private List<GameObject> _monsters;
        public List<GameObject> Monsters => _monsters;

        public Mythologie(string name, List<GameObject> monsters)
        {
            _name = name;
            _monsters = monsters;
            
            Debug.Log("mise en place de la mythologie : " + _name);
        }
        
        /*
         * Pouvoir spéciaux des mythologies :
         * - Japonaise : empêche les attaques des ennemies pendant un tour
         * - Nordique : double la puissance de toute les unités blessés
         * - Grecque : ?
         * - Egyptienne : empêche les déplacements des ennemies pendant un tour
         */

        public void PowerSpecial(Unit monster)
        {
            switch (_name)
            {
                case "Egyptienne":
                    Egyptienne(monster);
                    break;
                case "Grecque":
                    Grecque(monster);
                    break;
                case "Japonaise":
                    Japonaise(monster);
                    break;
                case "Nordique":
                    Nordique(monster);
                    break;
                default:
                    Debug.Log("Le nom de la mythologie ne correspond à aucune des mythologies répertoriées");
                    break;
            }
        }

        private void Egyptienne(Unit monster)
        {
            
        }

        private void Grecque(Unit monster)
        {
            
        }

        private void Japonaise(Unit monster)
        {
            
        }

        private void Nordique(Unit monster)
        {
            
        }
    }
}