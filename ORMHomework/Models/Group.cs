using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORMHomework.Models
{
    class Group
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(15)]
        public string Number { get; set; }

        [Required]
        public int Rate { get; set; }

        public ICollection<Student> Students { get; set; }
    
        public Faculty Faculty { get; set; }
        public int FacultyId { get; set; }
    }
}
