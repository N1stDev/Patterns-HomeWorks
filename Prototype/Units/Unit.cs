using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameIteration2.Units
{
    abstract internal class Unit: ICloneableUnit
    {
        public string unitName;
        public float attack;
        public float defense;
        public float hitPoints;
        //float maxHitPoints;

        public void TakeDamage(Unit opponent)
        {
            hitPoints -= opponent.attack;
        }

        public void SpecialAction(ISpecialAbility opponent)
        {
            if (opponent is Archer)
            {
                hitPoints -= opponent.specialStrength;
            }
            else if (opponent is Healer)
            {
                hitPoints += opponent.specialStrength;
            }
        }

        public string GetInfo()
        {
            return $"{unitName}, жизнь: {hitPoints}";
        }

        public abstract Unit Clone();
    }
}
