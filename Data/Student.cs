using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PassportNomber { get; set; }
        public School School { get; set; }

        public static Student CreatStudent(string Name, string Surname, string PassportNomber, School School)
        {
            var NewStudent = new Student()
            {
                Name = Name,
                PassportNomber = PassportNomber,
                School = School,
                Surname = Surname
            };
            return NewStudent;

        }
       
    }
}
