using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PabedaSchool.Data;
using PabedaSchool.Functions;

namespace PabedaSchool.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ApplicationDbContext db;


        public TestController(ApplicationDbContext context)
        {
            db = context;

        }

        public IActionResult Post()
        {
            TestFunctions.TestAll(db);
            return Ok();
        }
    }
}