using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleGameIteration2.Units;

namespace BattleGameIteration2
{
    internal class Army
    {
        public float cost = 0;
        public string name;
        public List<Unit> units = new();

        public Army(List<Unit> units, string name)
        {
            foreach (var u in units)
            {
                cost += u.attack + u.defense + u.hitPoints;
                if (u is ISpecialAbility)
                {
                    cost += (((ISpecialAbility)u).specialStrength + ((ISpecialAbility)u).specialRange) * 2;
                }
            }

            this.units = units;
            this.name = name;
        }

        public void CleanUpDead()
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].hitPoints <= 0)
                {
                    units.RemoveAt(i);
                }
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new();
            sb.Append($"Название армии: {name}\n");
            sb.Append($"Цена: {cost}\n");

            for (int i = 0; i < units.Count; i++)
            {
                sb.Append($"({i}) {units[i].GetInfo()}\n");
            }

            return sb.ToString();
        }
    }
}
