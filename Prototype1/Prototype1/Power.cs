using System.Collections.Generic;

namespace Prototype1
{
    public abstract class Power
    {
        public abstract void usePower();
    }

    public class Fusion : Power
    {
        public override void usePower()
        {
            throw new System.NotImplementedException();
        }
    }

    public class SpecialPower : Power
    {
        private bool targetAlly;
        private List<Monster> targets;
        private TargetStat targetStat;

        public SpecialPower(bool targetAlly, List<Monster> targets, TargetStat targetStat)
        {
            this.targetAlly = targetAlly;
            this.targets = targets;
            this.targetStat = targetStat;
        }
        
        public override void usePower()
        {
            foreach (var m in targets)//pour tout les monstres cibles
            {
                switch (targetStat)
                {
                        case TargetStat.STATE:
                            m.state = (targetAlly? (m.state +1): (m.state -1));
                            break;
                        
                        case TargetStat.STRENGTH:
                            break;
                        
                        case TargetStat.ATTACK:
                            m.attack = (targetAlly ? m.attack : 0);
                            break;
                        
                        case TargetStat.MOVEMENT:
                            m.movement = (targetAlly ? m.movement : 0);
                            break;
                }
            }
        }
    }

    public enum TargetStat
    {
        STATE,
        STRENGTH,
        ATTACK,
        MOVEMENT
    }
}