using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Mythologie : MonoBehaviour
    {
        private Mytho _name;
        public Mytho Name => _name;
        
        private List<GameObject> _monsters;
        public List<GameObject> Monsters => _monsters;

        private int indexPlayer;

        public bool activated;

        public Mythologie(int mythologie, List<GameObject> monsters, int id)
        {
            _name = (Mytho) mythologie;
            _monsters = monsters;
            activated = false;
            indexPlayer = id;
            
            Debug.Log("mise en place de la mythologie : " + _name + ", elle contient " + _monsters.Count + " monstres");
        }
        
        /*
         * Pouvoir spéciaux des mythologies :
         * - Japonaise : empêche les attaques des ennemies pendant un tour
         * - Nordique : double la puissance de toute les unités blessés
         * - Grecque : si l'unité gagne un combat, elle tue son ennemie
         * - Egyptienne : empêche les déplacements des ennemies pendant un tour
         */
		
        // Sélectionne la fonction qu'il faut appliquer selon la mythologie
        public void PowerSpecial(Unit monster, ref int power)
        {
            switch (_name)
            {
                case Mytho.Egyptienne:
                    Egyptienne(monster);
                    break;
                case Mytho.Grecque:
                    Grecque(monster);
                    break;
                case Mytho.Japonaise:
                    Japonaise(monster);
                    break;
                case Mytho.Nordique:
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
            if (activated && monster.Player != indexPlayer)
            {
                Debug.Log("Pouvoir de la mythologie Egyptienne appliqué sur " + monster.Name + ", (" + monster._movement.x + ", " + monster._movement.y + ")");
                
                monster._movement = Vector2.zero;
                
                if (monster._movement == Vector2.zero) Debug.Log("Réinitialisation du mouvement");
            }
            else Debug.Log(monster.Name + " n'est pas un ennemi, l'index de son joueur est : " + monster.Player);
        }

        // Grecque : si l'unité gagne un combat, elle tue son ennemie
        private void Grecque(Unit monster)
        {
            Debug.Log("Pouvoir de la mythologie Grecque appliqué sur " + monster.Name);
        }

        // Japonaise : empêche les attaques des ennemies pendant un tour
        private void Japonaise(Unit monster)
        {
            if (activated && monster.Player != indexPlayer)
            {
                Debug.Log("Pouvoir de la mythologie Japonaise appliqué sur " + monster.Name + ", (" + monster._attack.x + ", " + monster._attack.y + ")");
                
                monster._attack = Vector2.zero;
                
                if (monster._attack == Vector2.zero) Debug.Log("Réinitialisation de l'attaque");
            }
            else Debug.Log(monster.Name + " n'est pas un ennemi, l'index de son joueur est : " + monster.Player);
        }

        // Nordique : double la puissance de toute les unités blessés
        private void Nordique(Unit monster, ref int power)
        {
            if (activated && monster.Player == indexPlayer && monster.wounded)
            {
                power += monster.Power;

                Debug.Log("Pouvoir de la mythologie Nordique appliqué sur " + monster.Name + ", augmentation de la puissance d'attaque");
            }
            else Debug.Log(monster.Name + " n'est pas blessé, où est un ennemi");
        }

        public enum Mytho
        {
            Egyptienne,
            Grecque,
            Japonaise,
            Nordique
        }
        
        private Dictionary<Mytho, List<Tuple<string, int>>> listMonster = new Dictionary<Mytho, List<Tuple<string, int>>>()
	    {
	        { Mytho.Egyptienne, new List<Tuple<string, int>>()
	            {
	                new Tuple<string, int>("Nout", 3),
	                new Tuple<string, int>("Atoum", 3),
	                new Tuple<string, int>("Osiris", 3),
	                new Tuple<string, int>("Nephtys", 3),
	                new Tuple<string, int>("Anubis", 3),
	                new Tuple<string, int>("Isis", 3)
	            } },
		    { Mytho.Grecque, new List<Tuple<string, int>>()
	            {
	                new Tuple<string, int>("Minautore", 3),
	                new Tuple<string, int>("Sirene", 3),
	                new Tuple<string, int>("Harpie", 3),
	                new Tuple<string, int>("Cerbere", 3),
	                new Tuple<string, int>("Hydre", 3),
	                new Tuple<string, int>("Meduse", 3)
	            } },
		    { Mytho.Japonaise, new List<Tuple<string, int>>()
	            {
	                new Tuple<string, int>("Tatsu", 3),
	                new Tuple<string, int>("Kirin", 3),
	                new Tuple<string, int>("Kitsune", 3),
	                new Tuple<string, int>("Yuki-Onna", 3),
	                new Tuple<string, int>("Jorogumo", 3),
	                new Tuple<string, int>("Furi", 3)
	            } },
		    { Mytho.Nordique, new List<Tuple<string, int>>()
	            {
	                new Tuple<string, int>("Draugr", 3),
	                new Tuple<string, int>("Berserk", 3),
	                new Tuple<string, int>("Fenrir", 3),
	                new Tuple<string, int>("Troll", 3),
	                new Tuple<string, int>("Valkyrie", 3),
	                new Tuple<string, int>("Sorcier", 3)
	            }
	        }
	    };

	    public Tuple<string, int> GetArg(int index)
	    {
		    return listMonster[_name][index];
	    }
    }
}