using System;
using System.Collections.Generic;
using System.IO;

namespace Prototype1
{
    public class Mythology
    {
        private string name;
        private List<Monster> bestiary;

        public Mythology(int nbrBestiary, Player p)
        {
            bestiary = new List<Monster>();
            ReadBestiary(nbrBestiary, p);
        }

        private void ReadBestiary(int nbrBestiary, Player p)
        {
            string file = "../../../../bestiaries";

            string[] bestiaries = new StreamReader(file).ReadToEnd().Split('\n');//Tout les bestiaires

            if (nbrBestiary<0 || nbrBestiary >= bestiaries.Length)
                throw new Exception("");
            
            string[] monsters = bestiaries[nbrBestiary].Split('\t');//Tout les monstres

            {//TODO recuperer pouvoir special de la mythology
                string[] stats = monsters[0].Split('$');
                name = stats[0];
            }

            for(int i = 1; i < monsters.Length; i++)
            {
                string[] stats = monsters[i].Split('$');
                
                Monster m = new MonsterAlive(stats[0], p, Int32.Parse(stats[1]), new Fusion(), 0, 0 );
                //TODO power est censé être SpecialPower a mais il manque des infos pour le moment 
                
                bestiary.Add(m);
            }
        }
    }
}