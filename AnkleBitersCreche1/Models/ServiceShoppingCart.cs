using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnkleBitersCreche.Models
{
    public class ServiceShoppingCart
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int OurServiceId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("OurServiceId")]
        public virtual OurService OurService { get; set; }
    }
}
