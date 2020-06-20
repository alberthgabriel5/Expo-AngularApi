using System;
using System.Collections.Generic;

namespace WebAPILab.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Student = new HashSet<Student>();
        }

        public int NationalityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
