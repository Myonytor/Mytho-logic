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
            //TODO définir genos en fonction du nombre de joueurs
        }

        public void AddMonster(Monster monster)
        {
            _monsters[monster.x, monster.y] = monster;
        }

        public void MoveMonster(Monster monster)
        {
            _monsters[monster.x, monster.y] = null;
            switch (monster.movement)
            {
                case 1:
                    monster.x += 1;
                    break;
                case 2:
                    monster.x += 1;
                    monster.y += 1;
                    break;
                case 3:
                    monster.y += 1;
                    break;
                case 4:
                    monster.x -= 1;
                    break;
                case 5:
                    monster.x -= 1;
                    monster.y -= 1;
                    break;
                case 6:
                    monster.y -= 1;
                    break;
            }
            _monsters[monster.x, monster.y] = monster;
        }
    }

    public enum Tile
    {
        DISABLE,
        ENABLE,
        GENOS
    }
}