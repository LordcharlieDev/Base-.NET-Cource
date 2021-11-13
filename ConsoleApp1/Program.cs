using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1
{

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

    class Company : IEnumerable
    {
        List<Worker> workers = new List<Worker>()
        {
            new Worker("Sam", "Piter", "Jonsons", 1560, "Back-End", ".NET Developer", 2500),
            new Worker("Sam", "Piter", "Jonsons", 1660, "Test", ".NET Developer", 2500),
            new Worker("Sam", "Piter", "Jonsons", 1760, "Sell", ".NET Developer", 2500),
            new Worker("Sam", "Piter", "Jonsons", 1860, "Design", ".NET Developer", 2500),
        };
        List<string> departments = new List<string>();
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

        void FindAllDepartment()
        {
            foreach (var worker in workers)
            {
                foreach (var item in departments)
                {
                    if (worker.Department == item)
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
            FindAllDepartment();
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
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());
            if (FindNumber(number))
            {
                Console.Write("Enter new department's name:");
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
            List<int> counts = new List<int>();
            List<int> sumSalaries = new List<int>();
            int count = 0;
            int sum = 0;
            foreach (var depart in departments)
            {
                foreach (var worker in workers)
                {
                    if(depart == worker.Department)
                    {
                        count++;
                        sum += worker.Salary;
                    }
                }
                counts.Add(count);
                sumSalaries.Add(sum);
                for (int i = 0; i < departments.Count; i++)
                {
                    Console.WriteLine($"{departments[i]} department" +
                        $"\nCount of workers: {counts[i]}" +
                        $"\nFund salaries: {sumSalaries[i]}\n");
                }
                count = 0;
                sum = 0;
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

    }

    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company();
            company.PrintAll();
            company.AddWorker();
            company.PrintAll();
        }
    }
}
