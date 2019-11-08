using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Microsoft.EntityFrameworkCore;
using PabedaSchool.Data;

namespace Data
{
    public class DALSchool
    {
        public ApplicationDbContextData db;

        public DALSchool(ApplicationDbContextData context)
        {
            db = context;

        }
        public List<School> GetSchoolList()
        {
            var SchoolList = db.SchoolSet.ToList();
            return SchoolList;

        }

        public void AddSchool(School school)
        {
            db.SchoolSet.Add(school);
            db.SaveChanges();
        }
        public void UpdateSchool(School school)
        {
            db.Update(school);
            db.SaveChanges();

        }
        public void DeleteSchool(int Id)
        {
            var DeleteSchool = db.SchoolSet
                                         .Include(r => r.Teachers)
                                         .Include(r => r.Students)
                                         .Include(r => r.Classes)
                                         .FirstOrDefault(t => t.Id == Id);
            db.SchoolSet.Remove(DeleteSchool);
            db.SaveChanges();

        }
        public bool IsExist(int Id)
        {
            return db.SchoolSet.Any(r => r.Id == Id);

        }
        public School GetSchool(int Id)
        {

            return db.SchoolSet.Find(Id);
        }
        public bool IsExist(string Name)
        {
            return db.SchoolSet.Any(r => r.Name == Name);

        }
    }
}
