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
    public class AddTeacherController : ControllerBase
    {
        ITeacherLogic _teacherLogic;
     
        public AddTeacherController(ITeacherLogic teacherLogic)
        {
         
            _teacherLogic = teacherLogic;
        }

        public IActionResult Post(DataAPI dataAPI) // The Information of Teacher can Send as Object using Serialize json or as strings Data
        {

            var Result = _teacherLogic.AddNewTeacher( dataAPI.Name, dataAPI.Surname, dataAPI.PassportNomber, dataAPI.IdSchool);

            return Ok(Result);
        }


      
    }
}