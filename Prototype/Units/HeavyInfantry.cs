using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameIteration2.Units
{
    internal class HeavyInfantry : Unit
    {
        public HeavyInfantry()
        {
            unitName = "Тяжелый пехотинец";
            attack = 2;
            defense = 2;
            hitPoints = 2;
        }

        public HeavyInfantry(float hp)
        {
            unitName = "Тяжелый пехотинец";
            attack = 2;
            defense = 2;
            hitPoints = hp;
        }

        public override Unit Clone()
        {
            return new HeavyInfantry(this.hitPoints);
        }
    }
}
