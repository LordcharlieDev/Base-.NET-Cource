using System;
using System.Collections.Generic;

namespace ConsoleApp1
{

    class Worker
    {
        public string Firstname{ get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public int Number { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }

        public Worker() : this("Empty", "Empty", "Empty", 0, "Empty", "Empty", 0)
        {}
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

    class Company
    {
        List<Worker> workers = new List<Worker>()
        {
            new Worker("Sam", "Piter", "Jonsons", 1560, "IT", ".NET Developer", 2500),
            new Worker("Sam", "Piter", "Jonsons", 1660, "IT", ".NET Developer", 2500),
            new Worker("Sam", "Piter", "Jonsons", 1760, "IT", ".NET Developer", 2500),
            new Worker("Sam", "Piter", "Jonsons", 1860, "IT", ".NET Developer", 2500),
        };

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
                if(num == worker.Number)
                {
                    return true;
                }
            }
            return false; 
        }
        public AddWorker()
        {

        }

    }

    class ConnectDatabase
    {
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company();
            company.PrintAll();
        }
    }
}
