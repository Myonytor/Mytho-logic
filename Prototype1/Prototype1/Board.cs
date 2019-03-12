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

        public (int, int) Move(Monster monster)
        {
            //dans cette version, movement ne peut être égale qu'à 0, 3, 4, 7, -3, -4, -7
            return (monster.x + monster.movement % 2, monster.y + monster.movement % 3);
        }

        public void NextMove(List<Player> players)
        {
            HashSet<(int, int)> move = new HashSet<(int, int)>();
            HashSet<(int, int)> attack = new HashSet<(int, int)>();
            List<Monster> monsters = new List<Monster>();
            foreach (var player in players)
            {
                foreach (var monster in player.monstersAlive)
                {
                    (int x, int y) = Move(monster);

                    if (move.Contains((x, y)))
                    {
                        attack.Add((x, y));
                        move.Remove((x, y));
                    }
                    else move.Add((x, y));
                    
                    monsters.Add(monster);
                }
            }

            foreach (var monster in monsters)
            {
                (int x, int y) = Move(monster);
                
                if (move.Contains((x, y)))
                {
                    move.Remove((x, y));
                    monsters.Remove(monster);
                }
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