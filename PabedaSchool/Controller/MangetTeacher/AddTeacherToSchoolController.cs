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
    public class AddTeacherToSchoolController : ControllerBase
    {
        ITeacherLogic _teacherLogic;
      
        public AddTeacherToSchoolController( ITeacherLogic teacherLogic)
        {
         
            _teacherLogic = teacherLogic;
        }



        public IActionResult Post(DataAPI dataAPI) // The Information of Teacher can Send as Object using Serialize json or as strings Data
        {


            var Result = _teacherLogic.AddTeacherToSchool(dataAPI.IdSchool, dataAPI.IdTeacher);

            return Ok(Result);
        }

       
    }
}