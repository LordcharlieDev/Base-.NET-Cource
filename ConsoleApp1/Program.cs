using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp1
{
    [Serializable]
    class Worker
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = value > 0 ? value : 0;
            }
        }
        public string Department { get; set; }
        public string Position { get; set; }
        private int salary;
        public int Salary
        {
            get { return salary; }
            set
            {
                salary = value > 0 ? value : 0;
            }
        }
        public Worker() : this("Empty", "Empty", "Empty", 0, "Empty", "Empty", 0)
        { }
        public Worker(string firstname, string surname, string lastname, int number, string department, string position, int salary)
        {
            Firstname = firstname;
            Surname = surname;
            Lastname = lastname;
            Number = number;
            Department = department;
            Position = position;
            Salary = salary;
        }
        public override string ToString()
        {
            return $"Firstname: {Firstname} \nSurname: {Surname} \nLastname: {Lastname} " +
                $"\nNumber: {Number} \nDepartment: {Department} \nPosition: {Position} \nSalary: {Salary}\n";
        }
    }

    [Serializable]
    class Company : IEnumerable
    {
        List<Worker> workers = new List<Worker>()
        {
            new Worker("Artem", "Tomechek", "Mykolaiovych", 1560, "Back-End", ".NET Developer", 2500),
            new Worker("Bogdan", "Dargalchuk", "Edurdovych", 1660, "Front-End", "React.JS Developer", 2000),
            new Worker("Maks", "Ovodiuk", "Mykolaiovyck", 1760, "Front-End", "React.JS Developer", 2000),
            new Worker("Andrew", "Iurchuk", "Mykolaiovyck", 1860, "Test", "QA engineer", 1500),
            new Worker("Sam", "Piterson", "Jonsons", 1960, "Design", "UI/UX Designer", 1800),
        };
        List<string> departments = new List<string>();
        public int CountWorkers { get; set; }

        public void PrintAll()
        {
            foreach (Worker worker in workers)
            {
                Console.WriteLine(worker);
            }
        }
        bool FindNumber(int num)
        {
            foreach (Worker worker in workers)
            {
                if (num == worker.Number)
                {
                    return true;
                }
            }
            return false;
        }
        bool FindDepart(string depart)
        {
            foreach (var d in departments)
            {
                if(d == depart)
                {
                    return true;
                }
            }
            return false;
        }
        void FindAllDepartment()
        {
            departments.Clear();
            departments.Add(workers[0].Department);
            foreach (var worker in workers)
            {
                for (int i = 0; i < departments.Count; i++)
                {
                    
                    if (FindDepart(worker.Department))
                    {
                        break;
                    }
                    else
                    {
                        departments.Add(worker.Department);
                    }
                }
            }
        }
        public IEnumerator GetEnumerator()
        {
            return workers.GetEnumerator();
        }
        public void ClearAndFillWorkers()
        {
            workers.Clear();
            Console.Write("Enter the count of workers: ");
            int count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                AddWorker();
            }
            CountWorkers = workers.Count;
        }
        public void AddWorker()
        {
            string[] worker = new string[7];
            Console.Write("Enter firstname: ");
            worker[0] = Console.ReadLine();
            Console.Write("Enter surtname: ");
            worker[1] = Console.ReadLine();
            Console.Write("Enter lastname: ");
            worker[2] = Console.ReadLine();
            Console.Write("Enter number: ");
            while (true)
            {
                string number = Console.ReadLine();
                if (!FindNumber(int.Parse(number)))
                {
                    worker[3] = number.ToString();
                    break;
                }
                else
                {
                    Console.Write("Not unique! Enter again: ");
                }
            }
            Console.Write("Enter department: ");
            worker[4] = Console.ReadLine();
            Console.Write("Enter position: ");
            worker[5] = Console.ReadLine();
            Console.Write("Enter salary: ");
            worker[6] = Console.ReadLine();
            workers.Add(new Worker(worker[0], worker[1], worker[2], int.Parse(worker[3]), worker[4], worker[5], int.Parse(worker[6])));
            CountWorkers = workers.Count;
            Console.WriteLine("Worker added!");
        }
        public void RemoveWorker()
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());
            foreach (var worker in workers)
            {
                if (worker.Number == number)
                {
                    workers.Remove(worker);
                    CountWorkers = workers.Count;
                    Console.WriteLine("Worker removed!");
                    return;
                }
            }
            Console.WriteLine("No worker found! Check number!");
        }
        public void SortWorkerByDepartment()
        {
            workers.Sort(new DepatmentsComparer());
        }
        public void SortWorkerByPosition()
        {
            workers.Sort(new PositionComparer());
        }
        public void FindWorkerByFullname()
        {
            string[] workerWanted = new string[3];
            Console.Write("Enter firstname: ");
            workerWanted[0] = Console.ReadLine();
            Console.Write("Enter surtname: ");
            workerWanted[1] = Console.ReadLine();
            Console.Write("Enter lastname: ");
            workerWanted[2] = Console.ReadLine();
            foreach (var worker in workers)
            {
                if (worker.Firstname == workerWanted[0] && worker.Surname == workerWanted[1] && worker.Lastname == workerWanted[2])
                {
                    Console.WriteLine(worker);
                    return;
                }
            }
            Console.WriteLine("No worker found! Check full name!");
        }

        public void FindWorkersByDepartment()
        {
            FindAllDepartment();
            Console.Write("Enter department's name: ");
            string department = Console.ReadLine();
            foreach (var worker in workers)
            {
                if (worker.Department == department)
                {
                    Console.WriteLine(worker);
                }
            }
        }
        public void ChangeDepartmentForWorker()
        {
            FindAllDepartment();
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());
            if (FindNumber(number))
            {
                Console.Write("Enter new department's name: ");
                string department = Console.ReadLine();
                foreach (var depart in departments)
                {
                    if (depart == department)
                    {
                        for (int i = 0; i < workers.Count; i++)
                        {
                            if (workers[i].Number == number)
                            {
                                workers[i].Department = depart;
                                return;
                            }
                        }
                    }
                }
                Console.WriteLine("No department found! Check name!");
                return;
            }
            Console.WriteLine("No worker found! Check number!");
        }

        public void CountWorkersAndSumSalariesByDepartment()
        {
            FindAllDepartment();
            List<int> counts = new List<int>();
            List<int> sumSalaries = new List<int>();
            int count = 0;
            int sum = 0;
            foreach (var depart in departments)
            {
                foreach (var worker in workers)
                {
                    if (depart == worker.Department)
                    {
                        count++;
                        sum += worker.Salary;
                    }
                }
                counts.Add(count);
                sumSalaries.Add(sum);
                count = 0;
                sum = 0;
            }
            for (int i = 0; i < departments.Count; i++)
            {
                Console.WriteLine($"{departments[i]} department" +
                    $"\nCount of workers: {counts[i]}" +
                    $"\nFund salaries: {sumSalaries[i]}\n");
            }
            foreach (var worker in workers)
            {
                sum += worker.Salary;
            }
            Console.WriteLine($"The general fund of the company: {sum}");
        }
        public void CompanyReport()
        {
            CountWorkersAndSumSalariesByDepartment();
        }
    }
    class DepatmentsComparer : IComparer<Worker>
    {
        public int Compare(Worker x, Worker y)
        {
            return x.Department.CompareTo(y.Department);
        }
    }
    class PositionComparer : IComparer<Worker>
    {
        public int Compare(Worker x, Worker y)
        {
            return x.Position.CompareTo(y.Position);
        }
    }

    class Database
    {
        public void Save(Company company)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = File.Create("database.bin"))
            {
                binFormat.Serialize(fStream, company);
            }
        }
        public void Load(ref Company company)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = File.OpenRead("database.bin"))
            {
                company = (Company)binFormat.Deserialize(fStream);
            }
        }
    }

    class Menu
    {
        void PrintMenu()
        {
            Console.WriteLine("Human Resources Department");
            Console.WriteLine($"1 - Fill database");
            Console.WriteLine($"2 - Show all workers");
            Console.WriteLine($"3 - Add new worker");
            Console.WriteLine($"4 - Remove worker");
            Console.WriteLine($"5 - Sort workers by department");
            Console.WriteLine($"6 - Sort workers by position");
            Console.WriteLine($"7 - Search for a worker by full name");
            Console.WriteLine($"8 - Show workers by department");
            Console.WriteLine($"9 - Calculate the number of workers in the specified department and calculate the salary fund by department");
            Console.WriteLine($"10 - Delete reports on the dismissed employee");
            Console.WriteLine($"11 - Transfer of a worker to another department");
            Console.WriteLine($"12 - Company report");
            Console.WriteLine($"13 - Save");
            Console.WriteLine($"14 - Exit");
        }
        void ErrorMessage()
        {
            Console.Error.WriteLine("Undefined!");
            Console.ReadLine();
            Console.Clear();
        }
        public void CheckChosenMenu(Company company)
        {
            int num = 1;
            int g = 0;
            while (num != 14)
            {
                PrintMenu();
                Console.Write("Enter your choose: ");
                string choose = Console.ReadLine();
                if (int.TryParse(choose, out num))
                {
                    if (num == ++g)
                    {
                        company.ClearAndFillWorkers();
                    }
                    else if (num == ++g)
                    {
                        company.PrintAll();
                    }
                    else if (num == ++g)
                    {
                        company.AddWorker();
                    }
                    else if (num == ++g)
                    {
                        company.RemoveWorker();
                    }
                    else if (num == ++g)
                    {
                        company.SortWorkerByDepartment();
                    }
                    else if (num == ++g)
                    {
                        company.SortWorkerByPosition();
                    }
                    else if (num == ++g)
                    {
                        company.FindWorkerByFullname();
                    }
                    else if (num == ++g)
                    {
                        company.FindWorkersByDepartment();
                    }
                    else if (num == ++g)
                    {
                        company.CountWorkersAndSumSalariesByDepartment();
                    }
                    else if (num == ++g)
                    {
                        company.RemoveWorker();
                    }
                    else if (num == ++g)
                    {
                        company.ChangeDepartmentForWorker();
                    }
                    else if (num == ++g)
                    {
                        company.CompanyReport();
                    }
                    else if (num == ++g)
                    {
                        new Database().Save(company);
                    }
                    else if (num == ++g)
                    {
                        Console.WriteLine("Exiting...");
                        Console.Clear();
                    }
                }
                else
                {
                    ErrorMessage();
                }
                Console.ReadLine();
                Console.Clear();
                g = 0;
            }
            Console.WriteLine("Save database?\n" +
                "1 - Yes\n2 - No");
            string str = Console.ReadLine();
            if (int.TryParse(str, out num))
            {
                if (num == 1)
                {
                    new Database().Save(company);
                }
                Console.Clear();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Company company = null;
            Database database = new Database();
            Menu menu = new Menu();
            database.Load(ref company);
            menu.CheckChosenMenu(company);
        }
    }
}
