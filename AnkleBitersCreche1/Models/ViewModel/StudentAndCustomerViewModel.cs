using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkleBitersCreche1.Models.ViewModel
{
    public class StudentAndCustomerViewModel
    {
        public ApplicationUser UserObj { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
