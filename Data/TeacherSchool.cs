using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class TeacherSchool
    {
        public int Id { get; set; }
        public School School { get; set; }
        public Teacher Teacher { get; set; }
    }
}
