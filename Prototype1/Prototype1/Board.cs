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

            foreach (var player in players)
            {
                foreach (var monster in player.monstersAlive)
                {
                    (int x, int y) = monster.NextPos();

                    if (dictionary.ContainsKey((x, y)))
                    {
                        monster.x = x;
                        monster.y = y;
                    }
                    else
                    {
                        if (dictionary.ContainsKey((x, y)))
                        {
                            List<Monster> monsters = dictionary[(x, y)];
                            monsters.Add(monster);
                        }
                        else
                        {
                            dictionary.Add((x, y), new List<Monster>(){monster});   
                        }
                    }
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