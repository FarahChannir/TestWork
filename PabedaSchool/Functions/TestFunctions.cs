using Data;
using PabedaSchool.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PabedaSchool.Functions
{
    public static class TestFunctions
    {
        // //////This Function To Test All Functions without Call APIs

       
        public static void TestAll(ApplicationDbContext db)
        {
            ////// School/////////

            ///  Add School
            var Result1 = SchoolDataAccess.AddSchool(db, "School1", "Sirineevler", "Istanbul", "10 No");
            Debug.WriteLine(Result1);

            var Result2_1 = SchoolDataAccess.AddSchool(db, "School2", "Fatih", "Istanbul", "55 No");
            Debug.WriteLine(Result2_1);
            /////////
            var Result2_2 = SchoolDataAccess.AddSchool(db, "School3", "Fatih", "Istanbul", "55 No");
            Debug.WriteLine(Result2_2);

            ///Update School
            var School1 = db.SchoolSet.FirstOrDefault(t => t.Name == "School1");
            var Result3 = SchoolDataAccess.UpdateSchool(db, School1.Id, "Sirineevler", "Istanbul", "80 No");
            Debug.WriteLine(Result3);

            /////////

            ///////// Get List of Schools
            var Result4 = SchoolDataAccess.GetListSchools(db);
            Debug.WriteLine(Result4);

            /////////

            var School3 = db.SchoolSet.FirstOrDefault(t => t.Name == "School3");

            var Result5 = SchoolDataAccess.DeleteSchool(db, School3.Id);
            Debug.WriteLine(Result5);



            ////////// Teacher/////////

            /////////  Add Teacher to School1
            School1 = db.SchoolSet.FirstOrDefault(t => t.Name == "School1");

            var Result6 = TeacherDataAccess.AddNewTeacher(db, "Farah", "Channir", "25544", School1.Id);
            Debug.WriteLine(Result6);

            ///********************
            var Result7 = TeacherDataAccess.AddNewTeacher(db, "Hani", "Sami", "325235", School1.Id);
            Debug.WriteLine(Result7);

            // Add Teacher to School2
            var School2 = db.SchoolSet.FirstOrDefault(t => t.Name == "School2");

            var Result8 = TeacherDataAccess.AddNewTeacher(db, "Ahmed", "Omer", "657457", School2.Id);
            Debug.WriteLine(Result8);

            ///********************
            var Result9 = TeacherDataAccess.AddNewTeacher(db, "Layla", "Ali", "24324", School2.Id);
            Debug.WriteLine(Result9);

            ///////////////////
            // Add Teacher to Another School1
            var Teacher1 = db.TeacherSet.FirstOrDefault(y => y.Name == "Layla");
            var Result10 = TeacherDataAccess.AddTeacherToSchool(db, School1.Id, Teacher1.Id);
            Debug.WriteLine(Result10);

            ///////////////////Get List Teachers

            var Result11 = TeacherDataAccess.GetListTeachers(db);
            Debug.WriteLine(Result11);


            /////Student/////////
            School1 = db.SchoolSet.FirstOrDefault(t => t.Name == "School1");
            School2 = db.SchoolSet.FirstOrDefault(t => t.Name == "School2");
            // Add on Student To School
            var Result12 = StudentDataAccess.AddNewStudent(db, "Student1", "Surname1", "43543543", School1.Id);
            Debug.WriteLine(Result11);

            // Add List of Students

            List<DataAPI> Datas = new List<DataAPI>();
            Datas.Add(new DataAPI()
            {
                Name = "Student2",
                Surname = "Surname2",
                PassportNomber = "24234",
                IdSchool = School1.Id
            });
            Datas.Add(new DataAPI()
            {
                Name = "Student3",
                Surname = "Surname3",
                PassportNomber = "65765756",
                IdSchool = School2.Id
            });
            Datas.Add(new DataAPI()
            {
                Name = "Student4",
                Surname = "Surname4",
                PassportNomber = "87967767",
                IdSchool = School2.Id
            });

            var Result13 = StudentDataAccess.AddNewListStudents(db, Datas);
            Debug.WriteLine(Result13);

            /// Set Student To Teacher
            Teacher1 = db.TeacherSet.FirstOrDefault(y => y.Name == "Hani");
            var Teacher2 = db.TeacherSet.FirstOrDefault(y => y.Name == "Farah");
            var Student1 = db.StudentSet.FirstOrDefault(y => y.Name == "Student3");
            var Student2 = db.StudentSet.FirstOrDefault(y => y.Name == "Student1");

            var Result14 = StudentDataAccess.SetStudentToTeacher(db, School1.Id, Teacher1.Id, Student1.Id);
            Debug.WriteLine(Result14);

            var Result15 = StudentDataAccess.SetStudentToTeacher(db, School1.Id, Teacher2.Id, Student1.Id);
            Debug.WriteLine(Result15);


            var Result16 = StudentDataAccess.SetStudentToTeacher(db, School1.Id, Teacher2.Id, Student2.Id);
            Debug.WriteLine(Result16);
            ////Get List Students Of School
            var Result17 = StudentDataAccess.GetListStudentsOfSchool(db, School1.Id);
            Debug.WriteLine(Result17);

            ////Get List Students Of Teacher

            ///
            var Result18 = TeacherDataAccess.GetListStudentofTeacher(db, School1.Id, Teacher2.Id);
            Debug.WriteLine(Result18);

        }
    }
}
