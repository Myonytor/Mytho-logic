namespace Prototype1
{
    public class Monster
    {
        private State state;
        private int life;
        private int mana;

        private int movement;
        private int attack;

        private int x;
        private int y;

        public Monster()
        {
            state = State.Alive;
        }
    }

    enum State
    {
        Alive,
        Midlife,
        Dead
    }
}