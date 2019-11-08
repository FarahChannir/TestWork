using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IStudentLogic
    {
        string AddNewStudent(string Name, string Surname, string PassportNomber, int IdSchool);
        string AddNewListStudents(List<DataAPI> dataAPIs);
        string SetStudentToTeacher(int IdSchool, int IdTeacher, int IdStudent);
        List<Student> GetListStudentsOfSchool(int IdSchool);
    }
}
