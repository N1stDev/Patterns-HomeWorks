using BattleGameIteration2.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameIteration2
{
    internal class Battle
    {
        Random rnd = new (20);

        public Army Army1;
        public Army Army2;

        uint round = 1;
        public int unitPointer = 0;
        public bool winCondition = false;

        private static Battle? _instance = null;
        private static readonly object threadlock = new object();

        private Battle()
        {
        }

        public static Battle GetInstance()
        {
            lock (threadlock)
            {
                if (_instance == null)
                {
                    _instance = new Battle();
                }
                return _instance;
            }
        }

        public string MakeTurn()
        {
            StringBuilder sb = new();

            if (Army1.units.Count == 0)
            {
                winCondition = true;
                return ($"Битва завершена. Победила {Army2.name}!\n");
            }
            else if (Army2.units.Count == 0)
            {
                winCondition = true;
                return ($"Битва завершена. Победила {Army1.name}!\n");
            }
            // Если unitPointer указывает за предел одной из армий - завершаем текущий раунд
            if ((unitPointer > Army1.units.Count - 1) || (unitPointer > Army2.units.Count - 1))
            {
                round++;
                Army1.CleanUpDead();
                Army2.CleanUpDead();
                unitPointer = 0;
                (Army1, Army2) = (Army2, Army1);
                return ("Раунд завершен. Убираем мертвых. Армии поменялись местами в очередности.\n");
            }
            // Если это первый в очереди юнит
            if (unitPointer == 0)
            {
                CalulateFrontLiners(Army1, Army2);
                CalulateFrontLiners(Army2, Army1);

                unitPointer++;
                return sb.ToString();
            }

            // Просчитываем специальные атаки остальных юнитов
            CalculateSpecials(Army1, Army2);
            CalculateSpecials(Army2, Army1);

            unitPointer++;
            return sb.ToString();

            void CalulateFrontLiners(Army currentArmy, Army opposingArmy)
            {
                Unit currentUnit = currentArmy.units[0];

                if (currentArmy.units[0].hitPoints > 0)
                {
                    opposingArmy.units[0].TakeDamage(Army1.units[0]);
                    sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' ударяет '{opposingArmy.units[0].unitName}.'\n");
                }
                else
                {
                    sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' мертв и не может биться.\n");
                }
            }

            void CalculateSpecials(Army currentArmy, Army opposingArmy)
            {
                Unit currentUnit = currentArmy.units[unitPointer];

                sb.Append($"{unitPointer}");

                // Если юнит жив и обладает специальной атакой:
                if (currentUnit.hitPoints > 0 && currentUnit is ISpecialAbility)
                {
                    // Логика для лучника
                    if (currentUnit is Archer)
                    {
                        int hitPosition = -unitPointer - 1;
                        hitPosition += rnd.Next(1, ((ISpecialAbility)currentUnit).specialRange);

                        if (hitPosition >= 0 && hitPosition <= opposingArmy.units.Count - 1)
                        {
                            opposingArmy.units[hitPosition].SpecialAction((ISpecialAbility)currentUnit);

                            sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' попадает по '{opposingArmy.units[hitPosition].unitName}' армии '{opposingArmy.name}' на позиции {hitPosition}.\n");
                        }
                        else
                        {
                            sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' промахиватеся.\n");
                        }
                    }
                    // Логика для хилера
                    else if (currentUnit is Healer)
                    {
                        int leftBoundry = (int)Math.Ceiling((decimal)((ISpecialAbility)currentUnit).specialRange / 2);
                        int rightBoundry = (int)Math.Floor((decimal)((ISpecialAbility)currentUnit).specialRange / 2);

                        int healPosition = rnd.Next(unitPointer - leftBoundry, unitPointer + rightBoundry);

                        if (healPosition >= 0 && healPosition < currentArmy.units.Count())
                        {
                            currentArmy.units[healPosition].SpecialAction((ISpecialAbility)currentUnit);
                            sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' лечит '{currentArmy.units[healPosition].unitName}' на позиции {healPosition}.\n");
                        }
                        else
                        {
                            sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' промахиватеся и никого не лечит.\n");
                        }
                    }
                    // Логика для варлока
                    else if (currentUnit is Warlock)
                    {
                        int leftBoundry = (int)Math.Ceiling((decimal)((ISpecialAbility)currentUnit).specialRange / 2);
                        int rightBoundry = (int)Math.Floor((decimal)((ISpecialAbility)currentUnit).specialRange / 2);

                        int clonePosition = rnd.Next(unitPointer - leftBoundry, unitPointer + rightBoundry);

                        if (clonePosition >= 0 && clonePosition < currentArmy.units.Count())
                        {
                            // Вероятность клонирования!
                            // Клонирует себе вперёд.
                            currentArmy.units.Insert(unitPointer, currentArmy.units[clonePosition].Clone());
                            // После этого враг ходит не тем юнитом!
                            unitPointer++;
                            sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' клонирует '{currentArmy.units[clonePosition].unitName}' на позиции {clonePosition}.\n");
                        }
                        else
                        {
                            sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' промахиватеся и никого не клонирует.\n");
                        }
                    }
                }
                // Если жив, но не обладает специальной атакой:
                else if (currentUnit.hitPoints > 0 && currentUnit is not ISpecialAbility)
                {
                    sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' не имеет специальной атаки и пропускает ход.\n");
                }
                // Если мертв:
                else
                {
                    if (currentUnit is Healer)
                        sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' мёртв и не может лечить.\n");
                    else if (currentUnit is Warlock)
                        sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' мёртв и не может клонировать.\n");
                    else
                        sb.Append($"'{currentUnit.unitName}' армии '{currentArmy.name}' мёртв и не может биться.\n");
                }
            }
        }
    }
}
