using System.Collections.Generic;

namespace Prototype1
{
    public class Monster
    {
        private string name;
        private Player player;
        public int strength;
        public Power power;

        public Monster(string name, Player player, int strength, Power power)
        {
            this.name = name;
            this.player = player;
            this.strength = strength;
            this.power = power;
        }
    }

    public class MonsterAlive : Monster
    {

        public State state;
        public int movement;
        public int attack;

        public int x;
        public int y;
        
        public MonsterAlive(string name, Player player, int strength, Power power, int x, int y) : base(name, player, strength, power)
        {
            state = State.ALIVE;
            movement = attack = 0;
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