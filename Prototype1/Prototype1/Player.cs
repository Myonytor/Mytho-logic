using System.Collections.Generic;

namespace Prototype1
{
    public class Player
    {
        private string name;

        public string Name => name;

        private List<Monster> monstersAlive;
        private Mythology mythology;

        public Player(string n)
        {
            mythology = new Mythology();
            monstersAlive = new List<Monster>();
            name = n;
        }
    }
}