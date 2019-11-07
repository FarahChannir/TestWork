using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using PabedaSchool.Data;

namespace PabedaSchool.Functions
{
    public static class SchoolDataAccess
    {
        // The Set Function set in Strings Data to Used if the data come as strings not Object
        public static string AddSchool(ApplicationDbContext db, string Name, string Address, string City, string District)
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "The Name Of School Not Allow To be Empty";
                }
                else
                {
                    if (db.SchoolSet.FirstOrDefault(r => r.Name == Name) == null)
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
                        db.SchoolSet.Add(NewSchool);
                        db.SaveChanges();
                        return "true"; // Save is Done
                    }
                    else
                    {
                        return "This School Is Exist Before";
                    }
                }

            }
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "SetSchool",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false"; // Error 
            }

        }
        public static string UpdateSchool(ApplicationDbContext db, int Id, string Address, string City, string District)
        {
            try
            {
                var UpdateSchool = db.SchoolSet.FirstOrDefault(t => t.Id == Id);
                if (UpdateSchool != null)
                {
                    UpdateSchool.Address = Address;
                    UpdateSchool.City = City;
                    UpdateSchool.District = District;
                    db.Update(UpdateSchool);
                    db.SaveChanges();
                    return "true";
                }
                else
                {
                    return "The Id School is Not Exist";
                }

            }
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "UpdateSchool",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false";
            }

        }
        public static string DeleteSchool(ApplicationDbContext db, int Id)
        {
            try
            {
                var UpdateSchool = db.SchoolSet.FirstOrDefault(t => t.Id == Id);
                if (UpdateSchool != null)
                {
                    var DeleteSchool = db.SchoolSet
                                        .Include(r => r.Teachers)
                                        .Include(r => r.Students)
                                        .Include(r => r.Classes)
                                        .FirstOrDefault(t => t.Id == Id);
                    db.SchoolSet.Remove(DeleteSchool);
                    db.SaveChanges();
                    return "true";
                }
                else
                {
                    return "The Id School is Not Exist";
                }

            }
            catch (Exception Exp)
            {
                db.LogSystemSet.Add(new LogSystem()
                {
                    Date = DateTime.Now,
                    Scource = "DeleteSchool",
                    TextExption = Exp.Message,
                    TextIneerExption = Exp.InnerException?.Message
                });
                db.SaveChanges();
                return "false";
            }


        }

        public static List<School> GetListSchools(ApplicationDbContext db)
        {
            var ListSchool=db.SchoolSet.ToList();
            return ListSchool;
        }
    }
}
