using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public IList<TeacherSchool> Teachers { get; set; }
        public IList<Student> Students { get; set; }
        public IList<ClassSchool> Classes { get; set; }// This list allow me to get and save the students of specific Teacher and get Teacher of specific Student.
        public static School CreateStudent(string Name, string Address, string City, string District)
        {
            var NewSchool = new School()
            {
                Name = Name,
                Address = Address,
                City = City,
                District = District,
                Classes = new List<ClassSchool>(),
                Students = new List<Student>(),
                Teachers = new List<TeacherSchool>()
            };
            return NewSchool;

        }
    }
}
