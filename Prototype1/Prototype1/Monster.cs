using System.Collections.Generic;

namespace Prototype1
{
    public class Monster
    {
        private string name;
        private Player player;
        private State state;
        private int life;
        private int mana;

        public int movement;
        public int attack;

        public int x;
        public int y;

        public Monster(string name,Player p, int mana, int life, int x, int y)
        {
            this.name = name;
            state = State.ALIVE;
            player = p;
            movement = attack = 0;
            this.mana = mana;
            this.life = life;
            this.x = x;
            this.y = y;
        }
    }

    enum State
    {
        ALIVE,
        MIDLIFE,
        DEAD
    }
}