using System;
using System.Collections.Generic;
using System.Linq;

namespace Unification_Armies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Army army = new Army();
            army.Work();
        }
    }

    class Soldier
    {
        public Soldier()
        {
            Surname = UserUtils.GenerateRandomSurname();
        }

        public string Surname { get; private set; }
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static string GenerateRandomSurname()
        {
            string[] surnames = { "Букварёв", "Акишев", "Заварницын", "Быков", "Шашков" };
            string surname = "";
            return surname += surnames[_random.Next(surnames.Length)];
        }
    }

    class Army
    {
        private List<Soldier> _firstSquad = new List<Soldier>();
        private List<Soldier> _secondSquad = new List<Soldier>();

        public Army()
        {
            CreateSoldiers();
        }

        public void Work()
        {
            const string CommandUniteSquadsForSurname = "1";
            const string CommandExit = "2";

            bool isWork = true;

            while (isWork)
            {
                string letterB = "Б";
                Console.WriteLine("Первый отряд :");
                ShowSoldiersInSquad(_firstSquad);
                Console.WriteLine();
                Console.WriteLine("Второй отряд :");
                ShowSoldiersInSquad(_secondSquad);
                Console.WriteLine();
                Console.WriteLine($"Введите {CommandUniteSquadsForSurname}, чтобы объеденить во второй отряд солдат фамилия которых начинается на {letterB}");
                Console.WriteLine($"Введите {CommandExit}, чтобы завершить работу.");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandUniteSquadsForSurname:
                        UniteSquadsForSurname();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Нет такой команды...");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void CreateSoldiers()
        {
            int quantityOneSquadSoldiers = 10;

            for (int i = 0; i < quantityOneSquadSoldiers; i++)
            {
                _firstSquad.Add(new Soldier());
                _secondSquad.Add(new Soldier());
            }
        }

        private void UniteSquadsForSurname()
        {
            string letterB = "Б";
            var unionSquad = _secondSquad.Union(_firstSquad.Where(soldier => soldier.Surname.StartsWith(letterB))).ToList();
            _secondSquad = unionSquad;

            foreach (Soldier soldier in _secondSquad)
            {
                Console.WriteLine(soldier.Surname);
            }

            foreach (Soldier soldier in _firstSquad.Where(soldier => soldier.Surname.StartsWith(letterB)).ToList())
            {
                _firstSquad.Remove(soldier);
            }
        }

        private void ShowSoldiersInSquad(List<Soldier> squad)
        {
            foreach (Soldier soldier in squad)
            {
                Console.WriteLine(soldier.Surname);
            }
        }
    }
}