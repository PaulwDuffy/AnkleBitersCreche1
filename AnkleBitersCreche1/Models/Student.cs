using AnkleBitersCreche.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnkleBitersCreche.Models
{
    public class Student
    {

        [Key]
        public int Id { get; set; }

        [Required, MinLength (8), MaxLength(9)]
        public string PPS { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        public string UserId { get; set; }


        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
