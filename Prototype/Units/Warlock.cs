using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameIteration2.Units
{
    internal class Warlock : Unit, ISpecialAbility, ICloneableUnit
    {
        public int specialStrength { get; set; }
        public int specialRange { get; set; }

        public Warlock()
        {
            unitName = "Варлок";
            attack = 1;
            defense = 1;
            hitPoints = 2;
            specialStrength = 2;
            specialRange = 5;
        }

        public Warlock(float hp)
        {
            unitName = "Варлок";
            attack = 1;
            defense = 1;
            hitPoints = hp;
            specialStrength = 2;
            specialRange = 5;
        }

        public override Unit Clone()
        {
            return new Warlock(this.hitPoints);
        }
    }
}
