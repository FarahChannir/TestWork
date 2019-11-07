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
    public class StudentController : ControllerBase
    {

        private readonly ApplicationDbContext db;


        public StudentController(ApplicationDbContext context)
        {
            db = context;

        }

        public IActionResult PostAddStudent(DataAPI dataAPI) // The Information of Student can Send as Object using Serialize json or as strings Data
        {

            var Result = StudentDataAccess.AddNewStudent(db, dataAPI.Name, dataAPI.Surname, dataAPI.PassportNomber, dataAPI.IdSchool);

            return Ok(Result);
        }
        public IActionResult PostAddListNewStudents(List<DataAPI> DataAPIs) // The Information of Student can Send as Object using Serialize json or as strings Data
        {

            var Result = StudentDataAccess.AddNewListStudents(db, DataAPIs);
            // Result will be true for all Student Done Or false for Error Or List of Error in Sum Item
            return Ok(Result);
        }

        public IActionResult PostSetStudentToTeacher(DataAPI dataAPI)
        {
            var Result = StudentDataAccess.SetStudentToTeacher(db, dataAPI.IdSchool, dataAPI.IdTeacher, dataAPI.IdStudnet);
            return Ok(Result);
        }

        public IActionResult PostListSrudentsOfSchool(DataAPI dataAPI)
        {
            var ResultListStudent = StudentDataAccess.GetListStudentsOfSchool(db, dataAPI.IdSchool);
            return Ok(ResultListStudent);
        }
      
    }
}