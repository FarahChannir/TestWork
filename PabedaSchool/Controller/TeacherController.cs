using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PabedaSchool.Data;
using PabedaSchool.Functions;

namespace PabedaSchool.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext db;


        public TeacherController(ApplicationDbContext context)
        {
            db = context;

        }

        public IActionResult PostAddTeacher(DataAPI dataAPI) // The Information of Teacher can Send as Object using Serialize json or as strings Data
        {

            var Result = TeacherDataAccess.AddNewTeacher(db, dataAPI.Name, dataAPI.Surname, dataAPI.PassportNomber, dataAPI.IdSchool);

            return Ok(Result);
        }


        public IActionResult PostAddTeacherToSchool(DataAPI dataAPI) // The Information of Teacher can Send as Object using Serialize json or as strings Data
        {


            var Result = TeacherDataAccess.AddTeacherToSchool(db, dataAPI.IdSchool, dataAPI.IdTeacher);

            return Ok(Result);
        }

        public IActionResult PostGetListTeachers() // The Information of Teacher can Send as Object using Serialize json or as strings Data
        {
            var Result = TeacherDataAccess.GetListTeachers(db);

            return Ok(Result);
        }

        public IActionResult PostStudentsOfTeacher(DataAPI dataAPI)
        {
            var School = db.SchoolSet.FirstOrDefault(q => q.Id == dataAPI.IdSchool);
            var Teacher = db.TeacherSet.FirstOrDefault(q => q.Id == dataAPI.IdTeacher);
            if (School != null)
            {
                if (Teacher != null)
                {
                    var ListStudents = TeacherDataAccess.GetListStudentofTeacher(db, dataAPI.IdSchool, dataAPI.IdTeacher);
                    return Ok(ListStudents);
                }
                else
                {
                    return Ok("This Teacher Is Not Exist ");
                }
            }
            else
            {
                return Ok("This School Is Not Exist ");
            }
        }
    }
}