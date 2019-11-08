using System;
using System.Collections.Generic;
using System.Text;
using Data;
namespace Business.Interfaces
{
    public interface ITeacherLogic
    {
        List<Student> GetListStudentofTeacher(int IdSchool, int IdTeacher);
        List<Teacher> GetListTeachers();
        string AddTeacherToSchool(int IdSchool, int IdTeacher);
        string AddNewTeacher(string Name, string Surname, string PassportNomber, int IdSchool);
    }
}
