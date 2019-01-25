using System.Collections.Generic;

namespace Prototype1
{
    public class Player
    {
        private Monster[] bestiary;
        private List<Monster> monstersAlive;

        public Player()
        {
            bestiary = new Monster[] { };
            monstersAlive = new List<Monster>();
        }
    }
}