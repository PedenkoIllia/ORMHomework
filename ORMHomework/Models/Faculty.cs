using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORMHomework.Models
{
    class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Rate { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
