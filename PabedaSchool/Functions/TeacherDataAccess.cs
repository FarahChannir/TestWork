using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using PabedaSchool.Data;

namespace PabedaSchool.Functions
{
    public static class TeacherDataAccess
    {
        // The Set Function set in Strings Data to Used if the data come as strings not Object
        public static string AddNewTeacher(ApplicationDbContext db, string Name, string Surname, string PassportNomber, int IdSchool)
        {
            try
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(PassportNomber))
                {
                    return "All Informations Of Teacher Not Allow To be Empty";
                }
                else
                {
                    var School = db.SchoolSet.Include(t => t.Teachers).FirstOrDefault(q => q.Id == IdSchool);
                    if (School != null)
                    {
                        if (db.TeacherSet.FirstOrDefault(r => r.Name == Name && r.Surname == Surname) == null) // Not Allow to be more than one Teacher in same name in DB
                        {
                            var Teacher = new Teacher()
                            {
                                Name = Name,
                                Surname = Surname,
                                PassportNomber = PassportNomber,
                                Schools = new List<TeacherSchool>()
                            };

                            var TeacherSchool = new TeacherSchool()
                            {
                                School = School,
                                Teacher = Teacher
                            };
                            db.TeacherSet.Add(Teacher);
                            db.SaveChanges();

                            db.TeacherSchoolSet.Add(TeacherSchool);
                            db.SaveChanges();

                            return "true"; // Save is Done
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
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "AddTeacher",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false"; // Error 
            }

        }

        public static string AddTeacherToSchool(ApplicationDbContext db, int IdSchool, int IdTeacher)
        {
            try
            {

                var School = db.SchoolSet.Include(t => t.Teachers).FirstOrDefault(q => q.Id == IdSchool);
                var Teacher = db.TeacherSet.FirstOrDefault(q => q.Id == IdTeacher);
                if (School != null)
                {

                    if (Teacher != null)
                    {
                        if (db.TeacherSchoolSet.Include(r => r.School).Include(e => e.Teacher).FirstOrDefault(t => t.Teacher.Id == IdTeacher && t.School.Id == IdSchool) == null)
                        {
                            db.TeacherSchoolSet.Add(new TeacherSchool() { School = School, Teacher = Teacher });

                            db.SaveChanges();

                            return "true";
                        }
                        else
                        {
                            return "This Teacher Is already Added in this School";

                        }
                    }
                    else
                    {
                        return "This Teacher Is Not Exist ";
                    }

                }
                else
                {
                    return "This School Is Not Exist ";
                }


            }
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "AddTeacherToSchool",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false"; // Error 
            }

        }

        public static List<Teacher> GetListTeachers(ApplicationDbContext db)
        {
            var ListResult = db.TeacherSet.Include(t => t.Schools).ThenInclude(y => y.School).ToList();
            return ListResult;
        }

        public static List<Student> GetListStudentofTeacher(ApplicationDbContext db, int IdSchool, int IdTeacher)
        {

            var School = db.SchoolSet
                .Include(t => t.Classes).ThenInclude(y => y.Teacher)
                .Include(t => t.Classes).ThenInclude(y => y.Student)
                .FirstOrDefault(q => q.Id == IdSchool);
            var Teacher = db.TeacherSet.FirstOrDefault(q => q.Id == IdTeacher);
            var Students = School.Classes.Where(t => t.Teacher.Id == IdTeacher).Select(u => u.Student).ToList();
            return Students;

        }
    }
}
