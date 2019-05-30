using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Mythologie : MonoBehaviour
    {
        private string _name;
        public string Name => _name;
        
        private List<GameObject> _monsters;
        public List<GameObject> Monsters => _monsters;

        private int indexPlayer;

        public bool activated;

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

        public void PowerSpecial(Unit monster, ref int power)
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
                    Nordique(monster, ref power);
                    break;
                default:
                    Debug.Log("Le nom de la mythologie ne correspond à aucune des mythologies répertoriées");
                    break;
            }
        }
        
        // Egyptienne : empêche les déplacements des ennemies pendant un tour
        private void Egyptienne(Unit monster)
        {
            if (monster.Player != indexPlayer) monster._movement = Vector2.zero;
            
            Debug.Log("Pouvoir de la mythologie Egyptienne appliqué sur " + monster.Name);
        }

        // Grecque : ?
        private void Grecque(Unit monster)
        {
            Debug.Log("Pouvoir de la mythologie Grecque appliqué sur " + monster.Name);
        }

        // Japonaise : empêche les attaques des ennemies pendant un tour
        private void Japonaise(Unit monster)
        {
            if (monster.Player != indexPlayer) monster._attack = Vector2.zero;
            
            Debug.Log("Pouvoir de la mythologie Japonaise appliqué sur " + monster.Name);
        }

        // Nordique : double la puissance de toute les unités blessés
        private void Nordique(Unit monster, ref int power)
        {
            if (monster.Player == indexPlayer && monster.wounded) power += monster.Power; 
            
            Debug.Log("Pouvoir de la mythologie Nordique appliqué sur " + monster.Name);
        }
    }
}