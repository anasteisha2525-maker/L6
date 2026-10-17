using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_Task1
{
    // Інтерфейс для бази даних студентів
    interface IStudentDatabase
    {
        void SortAndDisplay();
        void DisplayTwins();
    }

    // Клас студента
    class Student : IStudentDatabase
    {
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty; // Формат ДД:ММ:РР
        public string BirthPlace { get; set; } = string.Empty;

        private List<Student> students;

        public Student()
        {
            students = new List<Student>
            {
                new Student("Коваленко", "12:05:04", "Київ"),
                new Student("Петренко", "03:11:03", "Львів"),
                new Student("Шевченко", "12:05:04", "Харків"),
                new Student("Мельник", "21:01:05", "Одеса"),
                new Student("Бойко", "03:11:03", "Дніпро")
            };
        }

        public Student(string lastName, string birthDate, string birthPlace)
        {
            LastName = lastName;
            BirthDate = birthDate;
            BirthPlace = birthPlace;
            students = new List<Student>(); // Ініціалізуємо порожнім списком для цього конструктора
        }

        // Допоміжний метод для переведення рядка ДД:ММ:РР у DateTime для сортування
        private DateTime ParseDate(string dateStr)
        {
            var parts = dateStr.Split(':');
            int day = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);
            int fullYear = year < 50 ? 2000 + year : 1900 + year;
            return new DateTime(fullYear, month, day);
        }

        // Реалізація методу інтерфейсу: сортування за зростанням дат народження та виведення таблиці
        public void SortAndDisplay()
        {
            var sorted = students.OrderBy(s => ParseDate(s.BirthDate)).ToList();

            Console.WriteLine("--- ТАБЛИЦЯ: Всі студенти за зростанням дати народження ---");
            Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", "Прізвище", "Дата народж.", "Місце народж.");
            Console.WriteLine(new string('-', 47));

            foreach (var st in sorted)
            {
                Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", st.LastName, st.BirthDate, st.BirthPlace);
            }
            Console.WriteLine();
        }

        // Реалізація методу інтерфейсу: виведення «двійнят» (співпадають день та місяць)
        public void DisplayTwins()
        {
            var twins = students
                .GroupBy(s => s.BirthDate.Substring(0, 5)) // Групуємо лише за ДД:ММ
                .Where(g => g.Count() > 1)
                .SelectMany(g => g)
                .ToList();

            Console.WriteLine("--- ТАБЛИЦЯ: «Двійнята» (однакові день і місяць народження) ---");
            if (twins.Count == 0)
            {
                Console.WriteLine("Студентів з однаковою датою народження не знайдено.");
                return;
            }

            Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", "Прізвище", "Дата народж.", "Місце народж.");
            Console.WriteLine(new string('-', 47));

            foreach (var st in twins)
            {
                Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", st.LastName, st.BirthDate, st.BirthPlace);
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IStudentDatabase db = new Student();
            db.SortAndDisplay();
            db.DisplayTwins();

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}