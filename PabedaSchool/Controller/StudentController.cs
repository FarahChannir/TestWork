using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PabedaSchool.Data;


namespace PabedaSchool.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {


        private IStudentLogic _studentLogic;

        public StudentController(IStudentLogic studentLogic)
        {

            _studentLogic = studentLogic;
        }
        //Add Student
        [HttpPost]
        public IActionResult Post(DataAPI dataAPI) // The Information of Student can Send as Object using Serialize json or as strings Data
        {

            var Result = _studentLogic.AddNewStudent(dataAPI.Name, dataAPI.Surname, dataAPI.PassportNomber, dataAPI.IdSchool);

            return Ok(Result);
        }
        [HttpPut]
        //Add List New Students
        public IActionResult Put(List<DataAPI> DataAPIs) // The Information of Student can Send as Object using Serialize json or as strings Data
        {

            var Result = _studentLogic.AddNewListStudents(DataAPIs);
            // Result will be true for all Student Done Or false for Error Or List of Error in Sum Item
            return Ok(Result);
        }
        [HttpGet]
        //Set Student To Teacher
        public IActionResult Get(DataAPI dataAPI)
        {
            var Result = _studentLogic.SetStudentToTeacher(dataAPI.IdSchool, dataAPI.IdTeacher, dataAPI.IdStudnet);
            return Ok(Result);
        }
        [HttpPost("ListStudentsOfSchool")]
        //List Students Of School
        public IActionResult PostListStudentsOfSchool(DataAPI dataAPI)
        {
            var ResultListStudent = _studentLogic.GetListStudentsOfSchool(dataAPI.IdSchool);
            return Ok(ResultListStudent);
        }

    }
}