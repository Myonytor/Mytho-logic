using System.Collections.Generic;

namespace Prototype1
{
    public class Monster
    {
        private string name;
        private Player player;
        public State state;
        public int strength;
        public Power power;

        public int movement;
        public int attack;

        public int x;
        public int y;

        public Monster(string name,Player p, int strength, int x, int y)
        {
            this.name = name;
            state = State.ALIVE;
            player = p;
            movement = attack = 0;
            this.strength = strength;
            this.x = x;
            this.y = y;
        }
        
        public (int, int) NextPos()
        {
            //dans cette version, movement ne peut être égale qu'à 0, 3, 4, 7, -3, -4, -7
            return (x + movement % 2, y + movement % 3);
        }
    }

    public enum State
    {
        DEAD = 0,
        MIDLIFE,
        ALIVE,
    }
}