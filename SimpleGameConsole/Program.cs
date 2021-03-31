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

            List<UnitBase> queueOrder;
            queueOrder = GetQueueOrder(player, monster);

            FightQueue queue = new FightQueue(queueOrder);
            ConductBattle(queue);
            Console.WriteLine($"Fight ended.");
        }

        private static List<UnitBase> GetQueueOrder(Player player, Monster monster)
        {
            int plInit = player.Initiative * RollD20();
            int moInit = monster.Initiative * RollD20();

            if (plInit >= moInit)
            {
                Console.WriteLine($"Unit {player.Name} starts");
                return new List<UnitBase> { player, monster };
            }
            else
            {
                Console.WriteLine($"Unit {monster.Name} starts");
                return new List<UnitBase> { monster, player };
            }
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
                if (queue.CurrentUnit() is Player player)
                {
                    Console.WriteLine("Choose type of attack: 1) Melee, 2) Magic");
                    ConsoleKeyInfo input = Console.ReadKey();

                    // TODO: Convert ConsoleKeyInfo value to AttackType enumerator.
                    dmg = CalculatePlayerDamage(player, input);
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

        private static int CalculatePlayerDamage(Player player, ConsoleKeyInfo input)
        {
            return input.Key == ConsoleKey.D1 
                ? player.Attack + RollD4() 
                : player.Magic + RollD6();
        }
    }
}
