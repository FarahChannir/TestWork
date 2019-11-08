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
        public static TeacherSchool CreaTeacherSchool(School School, Teacher Teacher)
        {
            var NewTeacherSchool = new TeacherSchool()
            {
                Teacher= Teacher,
                 School=School
            };
            return NewTeacherSchool;

        }
    }
}
