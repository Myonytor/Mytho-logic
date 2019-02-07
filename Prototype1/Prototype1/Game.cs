using System.Collections.Generic;

namespace Prototype1
{
    public class Game
    {
        private Tray tray;
        private List<Player> players;

        public Game(Tile[] t)
        {
            tray = new Tray(t);
            players = new List<Player>();
        }
    }
}