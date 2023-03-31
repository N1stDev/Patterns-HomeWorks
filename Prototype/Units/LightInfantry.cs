using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameIteration2.Units
{
    internal class LightInfantry : Unit, ICloneableUnit
    {
        public LightInfantry() 
        {
            unitName = "Легкий пехотинец";
            attack = 1;
            defense = 1;
            hitPoints = 2;
        }

        public LightInfantry(float hp)
        {
            unitName = "Легкий пехотинец";
            attack = 1;
            defense = 1;
            hitPoints = hp;
        }

        public override Unit Clone()
        {
            return new LightInfantry(this.hitPoints);
        }
    }
}
