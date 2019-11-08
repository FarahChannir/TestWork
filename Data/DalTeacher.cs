using Microsoft.EntityFrameworkCore;
using PabedaSchool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class DalTeacher
    {
        ApplicationDbContextData db;

        public DalTeacher(ApplicationDbContextData context)
        {
            db = context;

        }
       
        public List<Teacher> GetListTeachers()
        {
            var ListResult = db.TeacherSet.Include(t => t.Schools).ThenInclude(y => y.School).ToList();
            return ListResult;
        }

        public void AddNewTeacher(Teacher teacher)
        {
            db.TeacherSet.Add(teacher);
            db.SaveChanges();
        }

        public bool IsExist(string name, string surname)
        {

            return db.TeacherSet.Any(r => r.Name == name && r.Surname == surname);// Not Allow to be more than one Teacher in same name in DB

        }

        public void AddNewTeacherSchool(TeacherSchool teacherSchool)
        {
            db.TeacherSchoolSet.Add(teacherSchool);
            db.SaveChanges();
        }

        public bool IsExist(int idTeacher)
        {
            return db.TeacherSet.Any(r => r.Id == idTeacher);
        }

        public Teacher GetTeaher(int idTeacher)
        {
            return db.TeacherSet.Find(idTeacher);
        }

        public List<Student> GetListStudentofTeacher(int idSchool, int idTeacher)
        {
            var School = db.SchoolSet
                .Include(t => t.Classes).ThenInclude(y => y.Teacher)
                .Include(t => t.Classes).ThenInclude(y => y.Student)
                .FirstOrDefault(q => q.Id == idSchool);
            var Students = School.Classes.Where(t => t.Teacher.Id == idTeacher).Select(u => u.Student).ToList();
            return Students;
        }
    }
}
