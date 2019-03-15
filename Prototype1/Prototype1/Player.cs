using System.Collections.Generic;

namespace Prototype1
{
    public class Player
    {
        private string name;
        private const int nbrMonsterMax = 6;

        public string Name => name;
        
        private int xGenos;

        public int XGenos => xGenos;

        private int yGenos;

        public int YGenos => yGenos;

        public List<MonsterAlive> monstersAlive;
        private Mythology mythology;

        public Player(string n, int nbrMithology)
        {
            mythology = new Mythology(nbrMithology, this);
            monstersAlive = new List<MonsterAlive>();
            name = n;
        }

        public void CreateMonster()
        {
            
        }

        public void DestroyMonster()
        {
            
        }
    }
}