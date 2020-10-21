using System.ComponentModel.DataAnnotations;

namespace ORMHomework.Models
{
    class Student
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public Group Group { get; set; }
        public int? GroupId { get; set; }

        public Student(string firstName, string lastName, int? age = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Возраст: " + (Age == null ? "неизвестен." : $"{Age} лет");
        }
    }
}
