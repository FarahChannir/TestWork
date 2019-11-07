using Microsoft.EntityFrameworkCore;
using PabedaSchool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
namespace PabedaSchool.Functions
{
    public class StudentDataAccess
    {
        // The Set Function set in Strings Data to Used if the data come as strings not Object
        public static string AddNewStudent(ApplicationDbContext db, string Name, string Surname, string PassportNomber, int IdSchool)
        {
            try
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(PassportNomber))
                {
                    return "All Informations Of Teacher Not Allow To be Empty";
                }
                else
                {
                    var School = db.SchoolSet.Include(t => t.Students).FirstOrDefault(q => q.Id == IdSchool);
                    if (School != null)
                    {
                        if (db.StudentSet.FirstOrDefault(r => r.Name == Name && r.Surname == Surname) == null) // Not Allow to be more than one Student in same name in DB
                        {
                            var Student = new Student()
                            {
                                Name = Name,
                                Surname = Surname,
                                PassportNomber = PassportNomber,
                                School = School
                            };



                            db.StudentSet.Add(Student);
                            db.SaveChanges();


                            return "true"; // Save is Done
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
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "AddNewStudent",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false"; // Error 
            }

        }

        public static string AddNewListStudents(ApplicationDbContext db, List<DataAPI> dataAPIs)
        {
            string ErrorStrng = "";
            try
            {
                foreach (var item in dataAPIs)
                {

                    if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Surname) || string.IsNullOrEmpty(item.PassportNomber))
                    {
                        ErrorStrng += "All Informations Of Teacher Not Allow To be Empty in Item No :" + item.IdItem + " __ ";
                    }
                    else
                    {
                        var School = db.SchoolSet.Include(t => t.Students).FirstOrDefault(q => q.Id == item.IdSchool);
                        if (School != null)
                        {
                            if (db.StudentSet.FirstOrDefault(r => r.Name == item.Name && r.Surname == item.Surname) == null) // Not Allow to be more than one Student in same name in DB
                            {
                                var Student = new Student()
                                {
                                    Name = item.Name,
                                    Surname = item.Surname,
                                    PassportNomber = item.PassportNomber,
                                    School = School
                                };



                                db.StudentSet.Add(Student);


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
                db.SaveChanges();
                if (ErrorStrng == "")
                {
                    ErrorStrng = "true";// All Informations is Saved 
                }
                return ErrorStrng;
            }
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "AddNewListStudents",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false"; // Error 
            }
        }
        public static string SetStudentToTeacher(ApplicationDbContext db, int IdSchool, int IdTeacher, int IdStudent)
        {
            try
            {


                if (IdSchool != 0 && IdTeacher != 0 && IdStudent != 0)
                {
                    var School = db.SchoolSet.Include(t => t.Classes).FirstOrDefault(q => q.Id == IdSchool);
                    var Student = db.StudentSet.FirstOrDefault(q => q.Id == IdStudent);
                    var Teacher = db.TeacherSet.FirstOrDefault(q => q.Id == IdTeacher);
                    if (School == null)
                    {
                        return "This School Is Not Exist ";
                    }
                    else if (Student == null)
                    {
                        return "This Student Is Not Exist ";
                    }
                    else if (Teacher == null)
                    {
                        return "This Teacher Is Not Exist ";
                    }
                    ClassSchool Class = new ClassSchool()
                    {
                        Student = Student,
                        Teacher = Teacher
                    };
                    School.Classes.Add(Class);
                    db.Update(School);
                    db.SaveChanges();
                    var Schoolssss = db.SchoolSet.Include(t => t.Classes).FirstOrDefault(q => q.Id == IdSchool);

                    return "true";
                }
                else
                {
                    return "All Informations  Not Allow To be 0";
                }
            }
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "SetStudentToTeacher",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false"; // Error 
            }
        }
        public static List<Student> GetListStudentsOfSchool(ApplicationDbContext db, int IdSchool)
        {
            var ListResult = db.StudentSet.Include(r => r.School).Where(y => y.School.Id == IdSchool).ToList();
            return ListResult;
        }

    }
}
