using Business.Interfaces;
using Data;
using PabedaSchool.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class StudentLogic : IStudentLogic
    {

        DALSchool _dalschool;
        DALStudent _dal;
        DalTeacher _dalteacher;
     
        public StudentLogic(ApplicationDbContextData context)
        {
            _dal = new DALStudent(context);
            _dalschool = new DALSchool(context);
            _dalteacher = new DalTeacher(context);
        }
        public string AddNewListStudents(List<DataAPI> dataAPIs)
        {
            var ErrorStrng = "";
            foreach (var item in dataAPIs)
            {

                if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Surname) || string.IsNullOrEmpty(item.PassportNomber))
                {
                    ErrorStrng += "All Informations Of Student Not Allow To be Empty in Item No :" + item.IdItem + " __ ";
                }
                else
                {

                    if (_dalschool.IsExist(item.IdSchool))
                    {
                        if (!_dal.IsExist(item.Name, item.Surname)) // Not Allow to be more than one Student in same name in DB
                        {
                            var School = _dalschool.GetSchool(item.IdSchool);
                            
                          
                            _dal.AddNewStudent(Student.CreatStudent(item.Name, item.Surname, item.PassportNomber, School));
                        }
                        else
                        {
                            ErrorStrng += "This Student Is  Exist Before in Item No :" + item.IdItem + " __ ";
                        }
                    }
                    else
                    {
                        ErrorStrng += "This School Is Not Exist in Item No :" + item.IdItem + " __ ";
                    }

                }


            }

            if (ErrorStrng == "")
            {
                ErrorStrng = "true";// All Informations is Saved 
            }
            return ErrorStrng;
        }

        public string AddNewStudent(string Name, string Surname, string PassportNomber, int IdSchool)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(PassportNomber))
            {
                return "All Informations Of Students Not Allow To be Empty";
            }
            else
            {
                if (_dalschool.IsExist(IdSchool))
                {
                    if (!_dal.IsExist(Name, Surname))
                    {
                        var School = _dalschool.GetSchool(IdSchool);
                      
                        _dal.AddNewStudent(Student.CreatStudent(Name, Surname, PassportNomber, School));
                        return "true";
                    }
                    else
                    {
                        return "This Student Is  Exist Before";
                    }
                }
                else
                {
                    return "This School Is Not Exist ";
                }
            }
        }

        public List<Student> GetListStudentsOfSchool(int IdSchool)
        {
            if (!_dalschool.IsExist(IdSchool))
            {
                return null;

            }
            return _dal.GetListStudent(IdSchool);
        }

        public string SetStudentToTeacher(int IdSchool, int IdTeacher, int IdStudent)
        {
            if (!_dal.IsExist(IdStudent))
            {
                return "This Student Is Not Exist ";

            }
            if (!_dalschool.IsExist(IdSchool))
            {
                return "This School Is Not Exist ";

            }
            if (!_dalteacher.IsExist(IdTeacher))
            {
                return "This Teacher Is Not Exist ";
            }
            var Student = _dal.GetStudent(IdStudent);
            var Teacher = _dalteacher.GetTeaher(IdTeacher);

            ClassSchool Class = new ClassSchool()
            {
                Student = Student,
                Teacher = Teacher
            };
            _dal.SetStudntToTeacher(Class, IdSchool);
            return "true";
        }
    }
}
