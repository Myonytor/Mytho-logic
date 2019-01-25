using System.Collections.Generic;

namespace Prototype1
{
    public class game
    {
        private Tray tray;
        private List<Player> players;

        public game(int[] t)
        {
            tray = new Tray(t);
            players = new List<Player>();
        }
    }
}