using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PabedaSchool.Data;


namespace PabedaSchool.Controller.MangetTeacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsOfTeacherController : ControllerBase
    {
        ITeacherLogic _teacherLogic;
  
        public StudentsOfTeacherController( ITeacherLogic teacherLogic)
        {
      
            _teacherLogic = teacherLogic;
        }


        public IActionResult Post(DataAPI dataAPI)
        {

            var ListStudents = _teacherLogic.GetListStudentofTeacher(dataAPI.IdSchool, dataAPI.IdTeacher);
            return Ok(ListStudents);// null if there is wrong in IdSchool Or Teacher
        }
    }
}