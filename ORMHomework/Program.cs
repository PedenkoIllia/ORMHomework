using Microsoft.EntityFrameworkCore;
using ORMHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CreateDB().Result)
            {
                InitiateDb().Wait();
                ToDo().Wait();
            }

            Console.ReadLine();
        }

        static async Task<bool> CreateDB()
        {
            try
            {
                using (var db = new ApplicationDBContext())
                {
                    await db.Database.MigrateAsync();
                }

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        static async Task InitiateDb()
        {
            using (var db = new ApplicationDBContext())
            {
                if (!(db.Students.Any() || db.Groups.Any() || db.Faculties.Any()))
                {
                    var students = new List<Student>(12)
                    {
                        new Student("Джек", "Воробей", 40),
                        new Student("Чак", "Норис", 80),
                        new Student("Лара", "Крофт", 35),
                        new Student("Джеки", "Чан", 66),
                        new Student("Джим", "Керри", 58),
                        new Student("Мэри", "Сью"),
                        new Student("Том", "Сойер"),
                        new Student("Карл", "Маркс"),
                        new Student("Гермиона", "Грейнджер"),
                        new Student("Санта", "Клаус"),
                        new Student("Сэм", "Фишер", 63),
                        new Student("Эллен", "Рипли")
                    };

                    var groups = new List<Group>(4)
                    {
                        new Group()
                        {
                            Number = "101A",
                            Students = new List<Student>(students.Take(3)),
                            Rate = 89
                        },
                        new Group()
                        {
                            Number = "102В",
                            Students = new List<Student>(students.Skip(3).Take(3)),
                            Rate = 78
                        },
                        new Group()
                        {
                            Number = "201",
                            Students = new List<Student>(students.Skip(6).Take(3)),
                            Rate = 90
                        },
                        new Group()
                        {
                            Number = "203A",
                            Students = new List<Student>(students.Skip(9).Take(3)),
                            Rate = 84
                        }
                    };

                    var faculties = new List<Faculty>(2)
                    {
                        new Faculty
                        {
                            Name = "Факультет технической кибернетики",
                            Groups = new List<Group>(groups.Take(2)),
                            Rate = 83
                        },
                        new Faculty
                        {
                            Name = "Факультет иностранных языков",
                            Groups = new List<Group>(groups.Skip(2).Take(2)),
                            Rate = 87
                        }
                };

                    await using var transaction = await db.Database.BeginTransactionAsync();

                    await db.Faculties.AddRangeAsync(faculties); //faculties 1->* groups 1->* students 
                    await db.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
        }

        static async Task ToDo()
        {
            using (var db = new ApplicationDBContext())
            {
                var faculties = await db.Faculties.Include(f => f.Groups).ThenInclude(g => g.Students).AsNoTracking().ToListAsync();
                Console.WriteLine("Список факультетов:");
                foreach (Faculty faculty in faculties)
                {
                    Console.WriteLine(faculty.Name);
                    Console.WriteLine("Рейтинг факультета: " + faculty.Rate);

                    Console.WriteLine("\n\tСписок групп:");
                    foreach (Group group in faculty.Groups)
                    {
                        Console.WriteLine("\tГруппа №" + group.Number);
                        Console.WriteLine("\tРейтинг группы: " + group.Rate);

                        Console.WriteLine("\n\t\tСписок студентов:");
                        foreach (Student student in group.Students)
                        {
                            Console.WriteLine("\t\t" + student);
                        }
                        Console.WriteLine();
                    }
                }
            };
                    
        }
    }
}
