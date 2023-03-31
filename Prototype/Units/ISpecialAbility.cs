using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameIteration2.Units
{
    internal interface ISpecialAbility
    {
        public int specialStrength { get; set; }
        public int specialRange { get; set; }
    }
}
