using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkleBitersCreche1.Models.ViewModel
{
    public class StudentServiceViewModel
    {
        public Student Student { get; set; }
        public ServiceHeader ServiceHeader { get; set; }
        public ServiceDetails ServiceDetails { get; set; }

        public List<OurService> OurServiceList { get; set; }
        public List<ServiceShoppingCart> ServiceShoppingCart { get; set; }
    }
}
