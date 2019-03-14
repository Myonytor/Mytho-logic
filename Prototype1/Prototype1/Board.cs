using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Prototype1//TODO definir utilite
{
    public class Board
    {
        private Tile[,] board;
        private Monster[,] _monsters;

        public Board(Tile[,] b)
        {
            board = b;
            _monsters = new Monster[board.GetUpperBound(0), board.GetUpperBound(1)];
        }

        public void AddMonster(Monster monster)
        {
            _monsters[monster.x, monster.y] = monster;
        }

        public void NextMove(List<Player> players)
        {
            Dictionary<(int, int), List<Monster>> dictionary = new Dictionary<(int, int), List<Monster>>();
            foreach (var player in players)
            {
                foreach (var monster in player.monstersAlive)
                {
                    var nextPos = monster.NextPos();

                    if (dictionary.ContainsKey(nextPos))
                    {
                        dictionary[nextPos].Add(monster);
                    }
                    else
                    {
                        List<Monster> l = new List<Monster>();
                        l.Add(monster);
                        dictionary.Add(nextPos, l);
                    }
                }
            }

            foreach (KeyValuePair<(int,int), List<Monster>> kvp in dictionary)
            {
                if (kvp.Value.Count == 1)
                {
                    (int x, int y) = kvp.Key;
                    kvp.Value[0].x = x;
                    kvp.Value[0].y = y;
                    dictionary.Remove(kvp.Key);
                }
                else
                {
                    foreach (var monster in kvp.Value)
                        monster.attack = 0;
                }
            }
        
            //TODO mise en place des attaques des monstres qui se sont déjà déplacé

            foreach (var monsters in dictionary)
            {
                //TODO attaque des monstres qu'il reste sur la même case
            }
        }
    }

    public enum Tile
    {
        DISABLE,
        ENABLE,
        GENOS
    }
}