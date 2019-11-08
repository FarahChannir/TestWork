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
    public class SchoolController : ControllerBase
    {

        private ISchoolLogic _schoolLogic;

        public SchoolController(ISchoolLogic schoolLogic)
        {

            _schoolLogic = schoolLogic;
        }
        [HttpPost("AddSchool")]
        public IActionResult AddSchool() // The Information of School can Send as Object using Serialize json or as strings Data
        {
            DataAPI dataAPI = new DataAPI();

            var Result = _schoolLogic.AddSchool(dataAPI.Name, dataAPI.Address, dataAPI.City, dataAPI.District);

            return Ok(Result);// The Receive will Check the Result


        }
        [HttpPost("UpdateSchool")]
        public IActionResult UpdateSchool(DataAPI dataAPI) // The Information of School can Send as Object using Serialize json or as strings Data
        {

            var Result = _schoolLogic.UpdateSchool(dataAPI.IdSchool, dataAPI.Address, dataAPI.City, dataAPI.District);

            return Ok(Result);// The Receive will Check the Result
        }
        [HttpPost("DeleteSchool")]
        public IActionResult DeleteSchool(DataAPI dataAPI) // The Information of School can Send as Object using Serialize json or as strings Data
        {

            var Result = _schoolLogic.DeleteSchool(dataAPI.IdSchool);

            return Ok(Result);// The Receive will Check the Result
        }
        [HttpPost("GetListSchool")]

        public IActionResult GetListSchool()
        {
            return Ok(_schoolLogic.GetListSchools());
        }
    }
}