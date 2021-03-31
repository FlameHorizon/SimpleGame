using SimpleGameLib;
using System;
using System.Collections.Generic;

namespace SimpleGameConsole {
    public class Program {
        readonly static Random rng = new Random();
        static void Main(string[] args)
        {
            var player = new Player("Alice", hp: 50, attack: 4, initiative: 5, magic: 2);
            var monster = new Monster("Bob", 20, 5, 3, magic: 0);

            Console.WriteLine($"Your player name is {player.Name}");
            Console.WriteLine($"You stared fight with a monsert {monster.Name}");

            // Determine, who starts first.
            int plInit = player.Initiative * RollD20();
            int moInit = monster.Initiative * RollD20();

            List<UnitBase> units;
            if (plInit >= moInit)
            {
                units = new List<UnitBase> { player, monster };
                Console.WriteLine($"Unit {player.Name} starts");
            }
            else
            {
                units = new List<UnitBase> { monster, player };
                Console.WriteLine($"Unit {monster.Name} starts");
            }

            FightQueue queue = new FightQueue(units);
            ConductBattle(queue);
            Console.WriteLine($"Fight ended.");
        }

        private static int RollD20()
        {
            return rng.Next(1, 20);
        }

        private static int RollD4()
        {
            return rng.Next(1, 4);
        }

        private static int RollD6()
        {
            return rng.Next(1, 6);
        }

        private static void ConductBattle(FightQueue queue)
        {
            while (queue.AllAlive())
            {
                int dmg;
                if (queue.CurrentUnit() is Player)
                {
                    Console.WriteLine("Choose type of attack: 1) Melee, 2) Magic");
                    ConsoleKeyInfo input = Console.ReadKey();
                    if (input.Key == ConsoleKey.D1)
                        dmg = queue.CurrentUnit().Attack + RollD4();
                    else
                        dmg = queue.CurrentUnit().Magic + RollD6();
                }
                else
                    dmg = queue.CurrentUnit().Attack + RollD4();
                
                queue.NextUnit().Hp -= dmg;
                Console.WriteLine($"Unit {queue.CurrentUnit().Name} dealt {dmg} damage.");

                if (queue.NextUnit().IsAlive == false)
                {
                    Console.WriteLine($"Unit {queue.NextUnit().Name} is dead");
                    break;
                }
                queue.AdvanceQueue();
            }
        }
    }
}
