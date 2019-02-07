namespace Prototype1
{
    public class Monster
    {
        private Player player;
        private State state;
        private int life;
        private int mana;

        private int movement;
        private int attack;

        private int x;
        private int y;

        public Monster(Player p)
        {
            state = State.ALIVE;
            player = p;
        }
    }

    enum State
    {
        ALIVE,
        MIDLIFE,
        DEAD
    }
}