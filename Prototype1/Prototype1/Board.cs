using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Prototype1//TODO definir utilite
{
    public class Board
    {
        private Tile[,] board;
        private Monster[,] _monsters;
        private Mythology[,] _mythologies;
        private List<(Player, int, int)> genos;

        public Board(Tile[,] b)
        {
            board = b;
            int h = board.GetUpperBound(0);
            int w = board.GetUpperBound(1);
            _monsters = new Monster[h,w];
            _mythologies = new Mythology[h,w];
            //TODO définir genos en fonction du nombre de joueurs
        }

        public void AddMonster(Monster monster, Player player)
        {
            int x = 1;
            int y = 1;
            Random rnd = new Random();
            foreach (var geno in genos)
            {
                (Player p, int X, int Y) = geno;
                if (player.Name == p.Name)
                {
                    x = X - 1;
                    y = Y - 1;
                }
            }

            int n = rnd.Next(3);
            monster.x = x;
            monster.y = y;
            _monsters[x + n, y + n] = monster;
        }
    }

    public enum Tile
    {
        DISABLE,
        ENABLE,
        GENOS
    }
}