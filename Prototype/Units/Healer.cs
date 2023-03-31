using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameIteration2.Units
{
    internal class Healer : Unit, ISpecialAbility, ICloneableUnit
    {
        public int specialStrength { get; set; }
        public int specialRange { get; set; }

        public Healer()
        {
            unitName = "Целитель";
            attack = 1;
            defense = 1;
            hitPoints = 1;
            specialStrength = 2;
            specialRange = 5;
        }

        public Healer(float hp)
        {
            unitName = "Целитель";
            attack = 1;
            defense = 1;
            hitPoints = hp;
            specialStrength = 2;
            specialRange = 5;
        }

        public override Unit Clone()
        {
            return new Healer(this.hitPoints);
        }
    }
}
