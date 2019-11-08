using Microsoft.EntityFrameworkCore;
using PabedaSchool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class DALStudent
    {
       
        ApplicationDbContextData db;

        public DALStudent(ApplicationDbContextData context)
        {
            db = context;

        }
       
        public bool IsExist(string name, string surname)
        {
            return db.StudentSet.Any(t => t.Name == name && t.Surname == surname);
        }

        public void AddNewStudent(Student student)
        {
            db.StudentSet.Add(student);
            db.SaveChanges();
        }

        public bool IsExist(int idStudent)
        {
            return db.StudentSet.Any(t => t.Id == idStudent);
        }

        public Student GetStudent(int idStudent)
        {
            return db.StudentSet.FirstOrDefault(t => t.Id == idStudent);
        }

        public void SetStudntToTeacher(ClassSchool ClassSchool, int idSchool)
        {
            var School = db.SchoolSet.FirstOrDefault(t => t.Id == idSchool);
            School.Classes.Add(ClassSchool);
            db.Update(School);
            db.SaveChanges();
            
        }

        public List<Student> GetListStudent(int idSchool)
        {
            var ListResult = db.StudentSet.Include(r => r.School).Where(y => y.School.Id == idSchool).ToList();
            return ListResult;
        }
    }
}
