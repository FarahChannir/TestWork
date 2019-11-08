using Business.Interfaces;
using Data;
using PabedaSchool.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class TeacherLogic : ITeacherLogic
    {
        DalTeacher _dal;
        DALSchool _dalschool;

        public TeacherLogic(ApplicationDbContextData context)
        {
            _dal = new DalTeacher(context);
            _dalschool = new DALSchool(context);

        }

        public string AddNewTeacher(string Name, string Surname, string PassportNomber, int IdSchool)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(PassportNomber))
            {
                return "All Informations Of Teacher Not Allow To be Empty";
            }
            else
            {
                if (_dalschool.IsExist(IdSchool))
                {

                    if (!_dal.IsExist(Name, Surname))
                    {
                        var teacher = Teacher.CreaTeacher(Name, Surname, PassportNomber);
                        _dal.AddNewTeacher(teacher);
                        var School = _dalschool.GetSchool(IdSchool);

                        _dal.AddNewTeacherSchool(TeacherSchool.CreaTeacherSchool(School, teacher));
                        return "true";
                    }
                    else
                    {
                        return "This Teacher Is  Exist Before";
                    }
                }
                else
                {
                    return "This School Is Not Exist ";
                }
            }
        }

        public string AddTeacherToSchool(int IdSchool, int IdTeacher)
        {

            if (_dal.IsExist(IdTeacher))
            {

                if (_dalschool.IsExist(IdSchool))
                {
                    var School = _dalschool.GetSchool(IdSchool);
                    var Teacher = _dal.GetTeaher(IdTeacher);

                    _dal.AddNewTeacherSchool(TeacherSchool.CreaTeacherSchool(School, Teacher));
                    return "true";
                }
                else
                {
                    return "This Teacher Is  Exist Before";
                }
            }
            else
            {
                return "This School Is Not Exist ";
            }

        }

        public List<Student> GetListStudentofTeacher(int IdSchool, int IdTeacher)
        {
            if (_dal.IsExist(IdTeacher))
            {

                if (_dalschool.IsExist(IdSchool))
                {

                    return _dal.GetListStudentofTeacher(IdSchool, IdTeacher);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<Teacher> GetListTeachers()
        {
            return _dal.GetListTeachers();
        }
    }
}
