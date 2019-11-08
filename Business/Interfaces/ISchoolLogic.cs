using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
   public interface ISchoolLogic
    {
        List<School> GetListSchools();
        string AddSchool(string Name, string Address, string City, string District);
        string UpdateSchool(int Id, string Address, string City, string District);
        string DeleteSchool(int Id);
    }
}
