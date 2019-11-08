using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Passport_Nomber { get; set; }
        public IList<TeacherSchool> Schools { get; set; }
        public static Teacher CreaTeacher(string Name, string Surname, string PassportNomber)
        {
            var NewTeacher = new Teacher()
            {
                Name = Name,
                Passport_Nomber = PassportNomber,
                Schools = new List<TeacherSchool>(),
                Surname = Surname
            };
            return NewTeacher;

        }
    }
}
