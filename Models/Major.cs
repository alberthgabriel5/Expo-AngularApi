using System;
using System.Collections.Generic;

namespace WebAPILab.Models
{
    public partial class Major
    {
        public Major()
        {
            Student = new HashSet<Student>();
        }

        public int MajorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
