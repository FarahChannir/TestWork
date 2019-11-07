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
    public class SchoolController : ControllerBase
    {
        private readonly ApplicationDbContext db;


        public SchoolController(ApplicationDbContext context)
        {
            db = context;

        }

        public IActionResult PostAddSchool(DataAPI dataAPI) // The Information of School can Send as Object using Serialize json or as strings Data
        {

            var Result = SchoolDataAccess.AddSchool(db, dataAPI.Name, dataAPI.Address, dataAPI.City, dataAPI.District); 

            return Ok(Result);// The Receive will Check the Result

            
        }

        public IActionResult PostUpdateSchool(DataAPI dataAPI) // The Information of School can Send as Object using Serialize json or as strings Data
        {

            var Result = SchoolDataAccess.UpdateSchool(db, dataAPI.IdSchool, dataAPI.Address, dataAPI.City, dataAPI.District);

            return Ok(Result);// The Receive will Check the Result
        }

        public IActionResult PostDeleteSchool(DataAPI dataAPI) // The Information of School can Send as Object using Serialize json or as strings Data
        {

            var Result = SchoolDataAccess.DeleteSchool(db, dataAPI.IdSchool);

            return Ok(Result);// The Receive will Check the Result
        }

        public IActionResult PostGetListSchool()
        {
            return Ok(SchoolDataAccess.GetListSchools(db));
        }
    }
}