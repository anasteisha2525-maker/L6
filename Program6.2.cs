using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_Task2
{
    // Інтерфейс із абстрактними методами
    public interface IMedicalService
    {
        int GetTotalVisitors();
        void DisplayMinVisitorsAppointment();
    }

    // Базовий клас "Лікар"
    public class Doctor
    {
        public string LastName { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;

        public Doctor() { }

        public Doctor(string lastName, string speciality)
        {
            LastName = lastName;
            Speciality = speciality;
        }

        public int GetLastNameLength()
        {
            return LastName.Length;
        }
    }

    // Клас для опису одного прийому
    public class Appointment
    {
        public string Day { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;
        public int VisitorsCount { get; set; }

        public Appointment(string day, string shift, int visitorsCount)
        {
            Day = day;
            Shift = shift;
            VisitorsCount = visitorsCount;
        }
    }

    // Похідний клас, що реалізує інтерфейс
    public class DoctorSchedule : Doctor, IMedicalService
    {
        private List<Appointment> appointments = new List<Appointment>();

        public DoctorSchedule(string lastName, string speciality)
            : base(lastName, speciality) { }

        public void AddAppointment(string day, string shift, int visitors)
        {
            appointments.Add(new Appointment(day, shift, visitors));
        }

        public int GetTotalVisitors()
        {
            return appointments.Sum(a => a.VisitorsCount);
        }

        public void DisplayMinVisitorsAppointment()
        {
            if (appointments.Count == 0)
            {
                Console.WriteLine("Дані про прийоми відсутні.");
                return;
            }

            var minApp = appointments.OrderBy(a => a.VisitorsCount).First();
            Console.WriteLine($"Прийом з мін. кількістю відвідувачів ({minApp.VisitorsCount}): {minApp.Day}, зміна: {minApp.Shift}");
        }

        public void PrintFullReport()
        {
            Console.WriteLine($"\n================ ІНФОРМАЦІЯ ПРО ЛІКАРЯ ================");
            Console.WriteLine($"Лікар: {LastName} | Фах: {Speciality}");
            Console.WriteLine($"Довжина прізвища: {GetLastNameLength()} символів");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", "День", "Зміна", "Кількість");
            Console.WriteLine("-------------------------------------------------------");

            foreach (var app in appointments)
            {
                Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", app.Day, app.Shift, app.VisitorsCount);
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"Загальна кількість відвідувачів: {GetTotalVisitors()}");
            DisplayMinVisitorsAppointment();
            Console.WriteLine("=======================================================\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DoctorSchedule doctor = new DoctorSchedule("Петренко", "Кардіолог");
            doctor.AddAppointment("Понеділок", "Ранкова", 14);
            doctor.AddAppointment("Вівторок", "Вечірня", 6);
            doctor.AddAppointment("Середа", "Ранкова", 20);

            doctor.PrintFullReport();

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}