using System.Collections.Generic;

namespace Prototype1
{
    public class Game
    {
        private Board board;
        private List<Player> players;

        public Game(Tile[,] t)
        {
            board = new Board(t);
            players = new List<Player>();
        }
    }
}