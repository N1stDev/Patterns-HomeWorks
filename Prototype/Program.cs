using BattleGameIteration2;
using BattleGameIteration2.Units;

namespace BattleGameIteration2
{
    class Program
    {
        static void Main()
        {
            Army army1 = new( new() {
                new HeavyInfantry(),
                new Archer(),
                new HeavyInfantry(),
                new Warlock(),
                new LightInfantry(),
            }, "Синие");

            Army army2 = new(new() {
                new HeavyInfantry(),
                new Archer(),
                new HeavyInfantry(),
                new Healer(),
                new LightInfantry(),
            }, "Красные");

            Battle battle = Battle.GetInstance();
            battle.Army1 = army1;
            battle.Army2 = army2;

            Console.WriteLine("Начальная информация о юнитах: \n");
            Console.WriteLine(army1.GetInfo());
            Console.WriteLine(army2.GetInfo());
            Console.ReadKey();

            while (!battle.winCondition)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Сейчас ходят юниты под номером: {battle.unitPointer}\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(battle.MakeTurn());
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(army1.GetInfo());
                Console.WriteLine(army2.GetInfo());
                Console.ReadKey();
            }
        }
    }
}
